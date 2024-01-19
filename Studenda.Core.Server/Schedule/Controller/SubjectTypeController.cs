`   using Microsoft.AspNetCore.Http;
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
    }
}
