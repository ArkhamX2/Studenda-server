using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Server.Security.Controller
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
        [Route("groups")]
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
            var Course = DataContext.Courses.FirstOrDefault(x=>x.Id==id);
            return Course;
        }
        [Route("group/{id}")]
        [HttpGet]
        public ActionResult<Group> GetGroupById(int id)
        {
            var Group = DataContext.Groups.FirstOrDefault(x=>x.Id==id);
            return Group;
        }
    }
}
