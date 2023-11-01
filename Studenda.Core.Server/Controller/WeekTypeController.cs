using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Schedule.Management;

namespace Studenda.Core.Server.Controller
{
    [Route("weektype")]
    [ApiController]
    public class WeekTypeController : ControllerBase
    {
        public WeekTypeController(DataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            Configuration = configuration;
        }

        private DataContext DataContext { get; }
        private IConfiguration Configuration { get; }

        [Route("get")]
        [HttpGet]
        public ActionResult<List<WeekType>> GetAllDepartments()
        {
            var weektype = DataContext.WeekTypes.ToList();
            return weektype;
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public ActionResult<WeekType> GetDepartmentById(int id)
        {
            var weektype = DataContext.WeekTypes.FirstOrDefault(x => x.Id == id)!;
            return weektype;
        }
    }
}
