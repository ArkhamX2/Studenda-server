using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Account;
using Studenda.Core.Server.Security.Data.Transfer;

namespace Studenda.Core.Server.Security.Controller;

[Route("api/user")]
[ApiController]
public class UserController
{
    public UserController(DataContext dataContext)
    {
        DataContext=dataContext;
    }

    private DataContext DataContext { get; }

    [Route("GetUserFromSecurityResponse")]
    [HttpGet]
    public ActionResult<User> GetUserFromSecurityResponse(SecurityResponse response)
    {
        var user = DataContext.Users.FirstOrDefault(x => x.Email == response.Email);

        return user;
    }
}