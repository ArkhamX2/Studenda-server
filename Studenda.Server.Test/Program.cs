using Microsoft.EntityFrameworkCore;
using Studenda.Server.Data;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Common;

#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif

var configuration = new SqliteConfiguration("Data Source=000_debug_storage.db", isDebugMode);

Console.WriteLine("Starting INSERT test...");

await using (var context = new DataContext(configuration))
{
    await InitializeContext(context);

    var dept1 = new Department { Name = "Факультет 1" };
    var dept2 = new Department { Name = "Факультет 2" };
    var dept3 = new Department { Name = "Факультет 3" };

    context.Departments.AddRange(dept1, dept2, dept3);

    var crs1 = new Course { Grade = 0, Name = "Курс 1" };
    var crs2 = new Course { Grade = 1, Name = "Курс 2" };
    var crs3 = new Course { Grade = 2, Name = "Курс 3" };
    var crs4 = new Course { Grade = 3, Name = "Курс 4" };
    var crs5 = new Course { Grade = 4, Name = "Курс 5" };
    var crs6 = new Course { Grade = 5, Name = "Выпуск" };

    context.Courses.AddRange(crs1, crs2, crs3, crs4, crs5, crs6);

    var grp1 = new Group { Course = crs1, Department = dept1, Name = "Группа 1" };
    var grp2 = new Group { Course = crs1, Department = dept1, Name = "Группа 2" };
    var grp3 = new Group { Course = crs2, Department = dept1, Name = "Группа 3" };
    var grp4 = new Group { Course = crs3, Department = dept2, Name = "Группа 4" };
    var grp5 = new Group { Course = crs3, Department = dept2, Name = "Группа 5" };
    var grp6 = new Group { Course = crs4, Department = dept2, Name = "Группа 6" };
    var grp7 = new Group { Course = crs4, Department = dept3, Name = "Группа 7" };
    var grp8 = new Group { Course = crs5, Department = dept3, Name = "Группа 8" };
    var grp9 = new Group { Course = crs6, Department = dept3, Name = "Группа 9" };

    context.Groups.AddRange(grp1, grp2, grp3, grp4, grp5, grp6, grp7, grp8, grp9);

    await context.SaveChangesAsync();
}

Console.WriteLine("Starting SELECT test...");

await using (var context = new DataContext(configuration))
{
    await InitializeContext(context);

    try
    {
        // TODO: Найти более эффективный способ загрузки связанных моделей.
        var groups = await context.Groups
            .Include(group => group.Course)
            .Include(group => group.Department)
            .Include(group => group.Accounts)
            .ToListAsync();

        Console.WriteLine(groups.Count);
    }
    catch (InvalidOperationException exception)
    {
        Console.WriteLine(exception);
    }
}

Console.WriteLine("Completed!");
Console.ReadLine();

return;

async Task InitializeContext(DataContext context)
{
    var status = await context.TryInitializeAsync();

    if (!status)
    {
        throw new Exception("Context initialization error");
    }
}
