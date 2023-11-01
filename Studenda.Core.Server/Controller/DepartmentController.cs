using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Schedule;

namespace Studenda.Core.Server.Controller
{
    [Route("department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public DataContext DataContext { get; }
        public IConfiguration Connfiguration { get; }

        public DepartmentController(DataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            Connfiguration = configuration;
        }
        [Route("get")]
        [HttpGet]
        public ActionResult<List<Department>> GetAllDepartments()
        {
            var departments = DataContext.Departments.ToList();
            return departments;
        }
        [Route("get/{id}")]
        [HttpGet]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            var department = DataContext.Departments.FirstOrDefault(x => x.Id == id)!;
            return department;
        }
        [Route("post")]
        [HttpPost]
        public IActionResult PostSubjects([FromBody] List<Department> subjects)
        {
            try
            {
                DataContext.Departments.AddRange(subjects);
                DataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("update")]
        [HttpPut]
        public IActionResult UpdateSubjects([FromBody] List<Department> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var Subject = DataContext.Departments.FirstOrDefault(x => x.Id == subject.Id);
                    if (Subject != null)
                    {
                        DataContext.Departments.Update(Subject);
                    }
                    else
                    {
                        DataContext.Departments.Add(Subject!);
                    }
                }
                DataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteSubjects([FromBody] List<Department> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var Subject = DataContext.Departments.FirstOrDefault(x => x.Id == subject.Id);
                    if (Subject != null)
                    {
                        DataContext.Departments.Remove(Subject);
                    }
                }
                DataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
