using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Common;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayPositionController : ControllerBase
    {
        public DayPositionController(DataEntityService dataEntityService)
        {
                DataEntityService=dataEntityService;
        }

        public DataEntityService DataEntityService { get; }

        [HttpGet]
        public ActionResult<List<Course>> Get([FromBody] List<int> ids)
        {
            return DataEntityService.Get(DataEntityService.DataContext.Courses, ids);
        }
    }
}
