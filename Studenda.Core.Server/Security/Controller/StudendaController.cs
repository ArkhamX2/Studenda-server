using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Account;
using Studenda.Core.Model.Common;
using Studenda.Core.Server.Security.Data.Transfer;

namespace Studenda.Core.Server.Security.Controller
{   
    [Route("studenda/api")]
    [ApiController]
    public class StudendaController
    {
        public DataContext DataContext { get; }
        public IConfiguration Connfiguration { get; }

        public StudendaController(DataContext dataContext, IConfiguration configuration)
        { 
            DataContext=dataContext;
            Connfiguration = configuration;
        }
        

        [Route("GetUserFromSecurityResponse")]
        [HttpGet]
        public ActionResult<User> GetUserFromSecurityResponse(SecurityResponse response)
        {
            var user = DataContext.Users.FirstOrDefault(x => x.Email == response.Email);
            return user;
        }


    }
}
