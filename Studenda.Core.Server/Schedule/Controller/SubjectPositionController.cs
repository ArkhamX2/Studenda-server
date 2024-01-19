using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Controller
{
    [Route("api/subjectposition")]
    [ApiController]
    public class SubjectPositionController : ControllerBase
    {
        public SubjectPositionController( DataEntityService dataEntityService)
        {
            DataEntityService=dataEntityService;
        }

        public DataEntityService DataEntityService { get; }

        [HttpGet]
        public ActionResult<List<SubjectPosition>> Get([FromBody] List<int> ids)
        {
            return DataEntityService.Get(DataEntityService.DataContext.SubjectPositions, ids);
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<SubjectPosition> entities)
        {
            var status = DataEntityService.Set(DataEntityService.DataContext.SubjectPositions, entities);

            if (!status)
            {
                return BadRequest("No week types were saved!");
            }

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] List<int> ids)
        {
            var status = DataEntityService.Remove(DataEntityService.DataContext.SubjectPositions, ids);

            if (!status)
            {
                return BadRequest("No week types were deleted!");
            }

            return Ok();
        }
    }
}
