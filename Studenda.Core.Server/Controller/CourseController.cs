using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Server.Controller
{
    [Route("course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        public CourseController(DataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            Configuration = configuration;
        }

        private DataContext DataContext { get; }
        private IConfiguration Configuration { get; }
        [Route("get")]
        [HttpGet]
        public ActionResult<List<Course>> GetAllCourse()
        {
            var departments = DataContext.Courses.ToList();
            return departments;
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public ActionResult<Course> GetDepartmentById(int id)
        {
            var department = DataContext.Courses.FirstOrDefault(x => x.Id == id)!;
            return department;
        }

        [Route("post")]
        [HttpPost]
        public IActionResult PostSubjects([FromBody] List<Course> subjects)
        {
            try
            {
                DataContext.Courses.AddRange(subjects);
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
        public IActionResult UpdateSubjects([FromBody] List<Course> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var department = DataContext.Courses.FirstOrDefault(x => x.Id == subject.Id);

                    if (department != null)
                    {
                        DataContext.Courses.Update(department);
                    }
                    else
                    {
                        DataContext.Courses.Add(department!);
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

        [Route("delete")]
        [HttpDelete]
        public IActionResult DeleteSubjects([FromBody] List<Course> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var department = DataContext.Courses.FirstOrDefault(x => x.Id == subject.Id);

                    if (department != null)
                    {
                        DataContext.Courses.Remove(department);
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
