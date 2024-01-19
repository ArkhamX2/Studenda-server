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
    }
}
