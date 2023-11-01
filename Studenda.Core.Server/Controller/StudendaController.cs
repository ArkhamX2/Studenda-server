using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Studenda.Core.Data;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Schedule;

namespace Studenda.Core.Server.Controller
{
    [Route("studenda/api")]
    [ApiController]
    public class StudendaController
    {
        public DataContext DataContext { get; }
        public IConfiguration Connfiguration { get; }

        public StudendaController(DataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            Connfiguration = configuration;
        }
        [Route("departments")]
        [HttpGet]
        public ActionResult<List<Department>> GetAllDepartments()
        {
            var departments = DataContext.Departments.ToList();
            return departments;
        }
        [Route("courses")]
        [HttpGet]
        public ActionResult<List<Course>> GetAllCourses()
        {
            var Courses = DataContext.Courses.ToList();
            return Courses;
        }

        [HttpGet]
        public ActionResult<List<Group>> GetAllGroups()
        {
            var Groups = DataContext.Groups.ToList();
            return Groups;
        }
        [Route("department/{id}")]
        [HttpGet]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            var department = DataContext.Departments.FirstOrDefault(x => x.Id == id);
            return department;
        }
        [Route("course/{id}")]
        [HttpGet]
        public ActionResult<Course> GetCourseById(int id)
        {
            var Course = DataContext.Courses.FirstOrDefault(x => x.Id == id);
            return Course;
        }
        [Route("group/{id}")]
        [HttpGet]
        public ActionResult<Group> GetGroupById(int id)
        {
            var Group = DataContext.Groups.FirstOrDefault(x => x.Id == id);
            return Group;
        }
        [Route("schedule")]
        [HttpGet]
        public ActionResult<List<Subject>> GetSchedule([FromBody] Group group, [FromBody] int WeekType)
        {
            List<Subject> subjects = DataContext.Subjects.Where(x => x.Group == group && x.WeekType.Index == WeekType).OrderBy(x => x.DayPosition).ThenBy(x => x.SubjectPosition).ToList();
            return subjects;
        }

    }
}


