using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Server.Controller
{
    [Route("group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        public GroupController(DataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            Configuration = configuration;
        }

        private DataContext DataContext { get; }
        private IConfiguration Configuration { get; }

        [Route("get")]
        [HttpGet]
        public ActionResult<List<Group>> GetGroups()
        {
            return DataContext.Groups.ToList();
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public ActionResult<Group> GetGroupById(int id)
        {
            var group = DataContext.Groups.FirstOrDefault(x => x.Id == id)!;
            return group;
        }

        [Route("post")]
        [HttpPost]
        public IActionResult AddGroups([FromBody] List<Group> groups)
        {
            try
            {
                DataContext.Groups.AddRange(groups.ToList());
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
        public IActionResult UpdateGroups([FromBody] List<Group> groups)
        {
            try
            {
                foreach (var group in groups)
                {                   
                    if (DataContext.Groups.Any(u=>u.Id==group.Id))
                    {
                        DataContext.Groups.Update(group);
                    }
                    else
                    {
                        DataContext.Groups.Add(group!);
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
        public IActionResult DeleteGroups([FromBody] List<int> groupsId)
        {
            try
            {
                foreach (var id in groupsId)
                {
                    var group = DataContext.Groups.FirstOrDefault(x => x.Id == id);

                    if (group != null)
                    {
                        DataContext.Groups.Remove(group);
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
