using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Controller
{
    [Route("api/dayposition")]
    [ApiController]
    public class DayPositionController : ControllerBase
    {
        public DayPositionController(DataEntityService dataEntityService)
        {
                DataEntityService=dataEntityService;
        }

        public DataEntityService DataEntityService { get; }

        [HttpGet]
        public ActionResult<List<DayPosition>> Get([FromBody] List<int> ids)
        {
            return DataEntityService.Get(DataEntityService.DataContext.DayPositions, ids);
        }
        [HttpPost]
        public IActionResult Post([FromBody] List<DayPosition> entities)
        {
            var status = DataEntityService.Set(DataEntityService.DataContext.DayPositions, entities);

            if (!status)
            {
                return BadRequest("No week types were saved!");
            }

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] List<int> ids)
        {
            var status = DataEntityService.Remove(DataEntityService.DataContext.DayPositions, ids);

            if (!status)
            {
                return BadRequest("No week types were deleted!");
            }

            return Ok();
        }
    }
}
