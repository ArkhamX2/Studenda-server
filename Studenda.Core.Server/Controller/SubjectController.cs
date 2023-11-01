using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Schedule;

namespace Studenda.Core.Server.Controller
{
    [Route("subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        public DataContext DataContext { get; }
        public IConfiguration Connfiguration { get; }

        public SubjectController(DataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            Connfiguration = configuration;
        }
        [Route("post")]
        [HttpPost]
        public IActionResult PostSubjects([FromBody] List<Subject> subjects)
        {
            try
            {
                DataContext.Subjects.AddRange(subjects);
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
        public IActionResult UpdateSubjects([FromBody] List<Subject> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var Subject = DataContext.Subjects.FirstOrDefault(x => x.Id == subject.Id);
                    if (Subject != null)
                    {
                        DataContext.Subjects.Update(Subject);
                    }
                    else
                    {
                        DataContext.Subjects.Add(Subject!);
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
        public IActionResult DeleteSubjects([FromBody] List<Subject> subjects)
        {
            try
            {
                foreach(var subject in subjects)
                {
                    var Subject = DataContext.Subjects.FirstOrDefault(x=>x.Id==subject.Id);
                    if(Subject != null)
                    {
                        DataContext.Subjects.Remove(Subject);
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
        [Route("student/get/{groupId}")]
        [HttpGet]
        public ActionResult<List<Subject>> GetScheduleToStudent(int groupId, [FromBody] int WeekType)
        {
            List<Subject> subjects = DataContext.Subjects.Where(x => x.Group.Id == groupId && x.WeekType.Index == WeekType).OrderBy(x => x.DayPosition).ThenBy(x => x.SubjectPosition).ToList();
            return subjects;
        }
        [Route("teacher/id/{Id}")]
        [HttpGet]
        public ActionResult<List<Subject>> GetScheduleToTeacher(int Id, [FromBody] int WeekType)
        {
            List<Subject> subjects = DataContext.Subjects.Where(x => x.User!.Id == Id && x.WeekType.Index == WeekType).OrderBy(x => x.DayPosition).ThenBy(x => x.SubjectPosition).ToList();
            return subjects;
        }




    }
}
