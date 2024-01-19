using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Controller
{
    [Route("api/subjecttype")]
    [ApiController]
    public class SubjectTypeController : ControllerBase
    {
        public SubjectTypeController(DataEntityService dataEntityService)
        {
            DataEntityService= dataEntityService;
        }

        public DataEntityService DataEntityService { get; }

        [HttpGet]
        public ActionResult<List<SubjectType>> Get([FromBody] List<int> ids)
        {
            return DataEntityService.Get(DataEntityService.DataContext.SubjectTypes, ids);
        }
        [HttpPost]
        public IActionResult Post([FromBody] List<SubjectType> entities)
        {
            var status = DataEntityService.Set(DataEntityService.DataContext.SubjectTypes, entities);

            if (!status)
            {
                return BadRequest("No week types were saved!");
            }

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] List<int> ids)
        {
            var status = DataEntityService.Remove(DataEntityService.DataContext.SubjectTypes, ids);

            if (!status)
            {
                return BadRequest("No week types were deleted!");
            }

            return Ok();
        }

    }
}
