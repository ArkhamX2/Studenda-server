using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Common;

#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif

var configuration = new SqliteConfiguration("Data Source=000_debug_storage.db", isDebugMode);

Console.WriteLine("Starting INSERT test...");

using (var context = new DataContext(configuration))
{
    var dept1 = new Department { Name = "dept1" };
    var dept2 = new Department { Name = "dept2" };
    var dept3 = new Department { Name = "dept3" };

    context.Departments.AddRange(dept1, dept2, dept3);

    var crs1 = new Course { Department = dept1, Name = "crs1" };
    var crs2 = new Course { Department = dept1, Name = "crs2" };
    var crs3 = new Course { Department = dept2, Name = "crs3" };
    var crs4 = new Course { Department = dept2, Name = "crs4" };
    var crs5 = new Course { Department = dept3, Name = "crs5" };
    var crs6 = new Course { Department = dept3, Name = "crs6" };

    context.Courses.AddRange(crs1, crs2, crs3, crs4, crs5, crs6);

    var grp1 = new Group { Course = crs1, Name = "grp1" };
    var grp2 = new Group { Course = crs1, Name = "grp2" };
    var grp3 = new Group { Course = crs2, Name = "grp3" };
    var grp4 = new Group { Course = crs3, Name = "grp4" };
    var grp5 = new Group { Course = crs3, Name = "grp5" };
    var grp6 = new Group { Course = crs4, Name = "grp6" };
    var grp7 = new Group { Course = crs4, Name = "grp7" };
    var grp8 = new Group { Course = crs5, Name = "grp8" };
    var grp9 = new Group { Course = crs6, Name = "grp9" };

    context.Groups.AddRange(grp1, grp2, grp3, grp4, grp5, grp6, grp7, grp8, grp9);
    context.SaveChanges();
}

Console.WriteLine("Starting SELECT test...");

using (var context = new DataContext(configuration))
{
    try
    {
        // TODO: Найти более эффективный способ загрузки связанных моделей.
        var groups = context.Groups
            .Include(group => group.Course)
                .ThenInclude(course => course.Department)
            .Include(group => group.UserGroupLinks)
                .ThenInclude(link => link.User)
                    .ThenInclude(user => user.Role)
                        .ThenInclude(role => role.RolePermissionLinks)
                            .ThenInclude(link => link.Permission)
            .ToList();
        
        Console.WriteLine(groups.Count);
    }
    catch (InvalidOperationException exception)
    {
        Console.WriteLine(exception);
    }
}

Console.WriteLine("Completed!");
Console.ReadLine();
