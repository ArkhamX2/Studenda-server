using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data;
using Studenda.Core.Data.Transfer.Security;
using Studenda.Core.Model.Security;
using Studenda.Core.Model.Security.Management;
using Studenda.Core.Server.Security.Data;
using Studenda.Core.Server.Security.Service;
using Studenda.Core.Server.Security.Service.Token;

namespace Studenda.Core.Server.Security.Controller;

[Route("api/security")]
[ApiController]
public class SecurityController : ControllerBase
{
    public SecurityController(
        IConfiguration configuration,
        DataContext dataContext,
        IdentityContext identityContext,
        ITokenService tokenService,
        UserManager<Account> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        Configuration = configuration;
        DataContext = dataContext;
        IdentityContext = identityContext;
        TokenService = tokenService;
        UserManager = userManager;
        RoleManager = roleManager;
    }

    private IConfiguration Configuration { get; }
    private DataContext DataContext { get; }
    private IdentityContext IdentityContext { get; }
    private ITokenService TokenService { get; }
    private UserManager<Account> UserManager { get; }
    private RoleManager<IdentityRole> RoleManager { get; }

    [HttpPost("login")]
    public async Task<ActionResult<SecurityResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var account = await UserManager.FindByEmailAsync(request.Email);

        if (account is null)
        {
            return Unauthorized();
        }

        var isValidPassword = await UserManager.CheckPasswordAsync(account, request.Password);

        if (!isValidPassword)
        {
            return Unauthorized();
        }

        var roleIds = await IdentityContext.UserRoles
            .Where(userRole => userRole.UserId == account.Id)
            .Select(userRole => userRole.RoleId)
            .ToListAsync();

        var roles = IdentityContext.Roles.Where(role => roleIds.Contains(role.Id)).ToList();
        var accessToken = TokenService.CreateToken(account, roles);

        account.RefreshToken = Configuration.GenerateRefreshToken();
        account.RefreshTokenExpiryTime =
            DateTime.UtcNow.AddDays(Configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

        await IdentityContext.SaveChangesAsync();

        var user = await DataContext.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.IdentityId == account.Id);

        if (user is null)
        {
            return Unauthorized();
        }

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };

        var value = new SecurityResponse
        {
            User = user,
            Token = accessToken,
            RefreshToken = account.RefreshToken
        };

        return Ok(JsonSerializer.Serialize(value, options));
    }

    [HttpPost("register")]
    public async Task<ActionResult<SecurityResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var role = await DataContext.Roles.FirstOrDefaultAsync(role => role.Name == request.RoleName);

        if (role?.Name is null)
        {
            throw new Exception($"Role '{request.RoleName}' does not exists!");
        }

        var persistenceAccount = new Account
        {
            Email = request.Email,
            UserName = request.Email
        };

        var result = await UserManager.CreateAsync(persistenceAccount, request.Password);

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        if (!result.Succeeded)
        {
            return BadRequest(ModelState);
        }

        var account = await IdentityContext.Users.FirstOrDefaultAsync(account => account.Email == request.Email);

        if (account is null)
        {
            throw new Exception("Internal error! Please try again.");
        }

        var identityRole = new IdentityRole
        {
            Name = request.RoleName
        };

        await RoleManager.CreateAsync(identityRole);
        await UserManager.AddToRoleAsync(account, request.RoleName);
        await DataContext.Users.AddAsync(new User
        {
            RoleId = role.Id,
            IdentityId = account.Id
        });

        await DataContext.SaveChangesAsync();

        // TODO: Использовать сервис, а не перекладывать ответственность на контроллер.
        return await Login(new LoginRequest
        {
            Email = request.Email,
            Password = request.Password,
            RoleName = request.RoleName
        });
    }

    [HttpPost]
    [Route("token/refresh")]
    public async Task<IActionResult> RefreshToken(TokenPair? tokenPair)
    {
        if (tokenPair is null)
        {
            return BadRequest("Invalid client request");
        }

        var accessToken = tokenPair.AccessToken;
        var refreshToken = tokenPair.RefreshToken;
        var principal = Configuration.GetPrincipalFromExpiredToken(accessToken);

        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        // TODO: это не выглядит безопасным.
        var account = await UserManager.FindByNameAsync(principal.Identity!.Name!);

        if (account == null || account.RefreshToken != refreshToken ||
            account.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = Configuration.CreateToken(principal.Claims.ToList());
        var newRefreshToken = Configuration.GenerateRefreshToken();

        account.RefreshToken = newRefreshToken;

        await UserManager.UpdateAsync(account);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost]
    [Route("token/revoke/{accountName}")]
    public async Task<IActionResult> RevokeToken(string accountName)
    {
        var account = await UserManager.FindByNameAsync(accountName);

        if (account == null)
        {
            return BadRequest("Invalid account name");
        }

        account.RefreshToken = null;

        await UserManager.UpdateAsync(account);

        return Ok();
    }

    [Authorize]
    [HttpPost]
    [Route("token/revoke")]
    public async Task<IActionResult> RevokeAllTokens()
    {
        var accountList = UserManager.Users.ToList();

        foreach (var account in accountList)
        {
            account.RefreshToken = null;

            // TODO: очень ресурсозатратно. доработать.
            await UserManager.UpdateAsync(account);
        }

        return Ok();
    }

    [HttpGet]
    [Route("test")]
    public async Task Test()
    {
        Response.ContentType = "text/html;charset=utf-8";

        await Response.WriteAsync("<h2>Hello METANIT.COM</h2>");
    }
}