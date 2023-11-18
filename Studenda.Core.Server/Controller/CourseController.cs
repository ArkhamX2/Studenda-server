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
        public ActionResult<List<Course>> GetCourses()
        {
            return DataContext.Courses.ToList();
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public ActionResult<Course> GetCourseById(int id)
        {
            var course = DataContext.Courses.FirstOrDefault(x => x.Id == id)!;
            return course;
        }

        [Route("post")]
        [HttpPost]
        public IActionResult AddCourses([FromBody] List<Course> courses)
        {
            try
            {
                DataContext.Courses.AddRange(courses);
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
        public IActionResult UpdateCourses([FromBody] List<Course> courses)
        {
            try
            {
                foreach (var subject in courses)
                {
                    var course = DataContext.Courses.FirstOrDefault(x => x.Id == subject.Id);

                    if (course != null)
                    {
                        DataContext.Courses.Update(course);
                    }
                    else
                    {
                        DataContext.Courses.Add(course!);
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
        public IActionResult DeleteCourses([FromBody] List<int> coursesId)
        {
            try
            {
                foreach (var id in coursesId)
                {
                    var course = DataContext.Courses.FirstOrDefault(x => x.Id == id);

                    if (course != null)
                    {
                        DataContext.Courses.Remove(course);
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
