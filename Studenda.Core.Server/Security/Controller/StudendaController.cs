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
            DataContext=dataContext;
            Connfiguration = configuration;
        }
        [Route("Departments")]
        [HttpGet]
        public ActionResult<List<Department>> GetAllDepartments()
        {
            var departments = DataContext.Departments.ToList();
            return departments;
        }
        [Route("Courses")]
        [HttpGet]
        public ActionResult<List<Course>> GetAllCourses()
        {
            var Courses = DataContext.Courses.ToList();
            return Courses;
        }
        [Route("Groups")]
        [HttpGet]
        public ActionResult<List<Group>> GetAllGroups()
        {
            var Groups = DataContext.Groups.ToList();
            return Groups;
        }


    }
}
