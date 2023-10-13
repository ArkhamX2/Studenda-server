using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Data.Transfer.Security;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Server.Security.Controller;

[Route("api/user")]
[ApiController]
public class UserController
{
    public UserController(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    private DataContext DataContext { get; }

    [Route("GetUserFromSecurityResponse")]
    [HttpGet]
    public ActionResult<User> GetUserFromSecurityResponse(SecurityResponse response)
    {
        var user = DataContext.Users.FirstOrDefault(user => user.IdentityId == response.User.IdentityId);

        return user;
    }
}