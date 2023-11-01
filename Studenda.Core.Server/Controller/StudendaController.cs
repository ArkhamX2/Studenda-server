using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Schedule;

namespace Studenda.Core.Server.Controller;

[Route("studenda/api")]
[ApiController]
public class StudendaController
{
    public StudendaController(DataContext dataContext, IConfiguration configuration)
    {
        DataContext = dataContext;
        Connfiguration = configuration;
    }

    private DataContext DataContext { get; }
    private IConfiguration Connfiguration { get; }

    [Route("departments")]
    [HttpGet]
    public ActionResult<List<Department>> GetAllDepartments()
    {
        return DataContext.Departments.ToList();
    }

    [Route("courses")]
    [HttpGet]
    public ActionResult<List<Course>> GetAllCourses()
    {
        return DataContext.Courses.ToList();
    }

    [Route("groups")]
    [HttpGet]
    public ActionResult<List<Group>> GetAllGroups()
    {
        return DataContext.Groups.ToList();
    }

    [Route("department/{id:int}")]
    [HttpGet]
    public ActionResult<Department?> GetDepartmentById(int id)
    {
        return DataContext.Departments.FirstOrDefault(x => x.Id == id);
    }

    [Route("course/{id:int}")]
    [HttpGet]
    public ActionResult<Course?> GetCourseById(int id)
    {
        return DataContext.Courses.FirstOrDefault(x => x.Id == id);
    }

    [Route("group/{id:int}")]
    [HttpGet]
    public ActionResult<Group?> GetGroupById(int id)
    {
        return DataContext.Groups.FirstOrDefault(x => x.Id == id);
    }

    [Route("schedule/{weekTypeIndex:int}")]
    [HttpGet]
    public ActionResult<List<Subject>> GetSchedule(int weekTypeIndex, [FromBody] Group group)
    {
        return DataContext.Subjects.Where(x => x.Group == group && x.WeekType.Index == weekTypeIndex).OrderBy(x => x.DayPosition).ThenBy(x => x.SubjectPosition).ToList();
    }
}