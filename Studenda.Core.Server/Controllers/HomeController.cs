using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Server.Controllers
{

    public class HomeController : Controller
    {
        private readonly DataContext context;

        public HomeController(DataContext _context)
        {
            context = _context;
        }

        [HttpGet("user/{id}")]
        public IActionResult GetId(int id)
        {
            return Json(context.Courses);
        }
    }
}
