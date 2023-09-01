using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studenda.Core.Model.Account;
using Studenda.Core.Server.Security.Data;
using Studenda.Core.Server.Security.Data.Transfer;
using Studenda.Core.Server.Security.Service;
using Studenda.Core.Server.Security.Service.Token;

namespace Studenda.Core.Server.Security.Controller;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IdentityContext identityContext;
    private readonly ITokenService tokenService;
    private readonly UserManager<Account> userManager;

    public AccountController(ITokenService tokenService, IdentityContext identityContext,
        UserManager<Account> userManager, IConfiguration configuration)
    {
        this.tokenService = tokenService;
        this.identityContext = identityContext;
        this.userManager = userManager;
        this.configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult<SecurityResponse>> Authenticate([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var persistenceAccount = await userManager.FindByEmailAsync(request.Email);

        if (persistenceAccount == null)
        {
            return BadRequest("Bad credentials");
        }

        var isValidPassword = await userManager.CheckPasswordAsync(persistenceAccount, request.Password);

        if (!isValidPassword)
        {
            return BadRequest("Bad credentials");
        }

        // TODO: зачем второй раз получать аккаунт?
        var mapper = new Mapper(new MapperConfiguration(expression => expression.CreateMap<User, Account>()));
        var account = mapper.Map<Account>(identityContext
            .Users.FirstOrDefault(account => account.Email == request.Email));

        if (account is null)
        {
            return Unauthorized();
        }

        var roleIds = await identityContext.UserRoles
            .Where(r => r.UserId == account.Id)
            .Select(x => x.RoleId)
            .ToListAsync();
        var roles = identityContext.Roles.Where(role => roleIds.Contains(role.Id)).ToList();

        var accessToken = tokenService.CreateToken(account, roles);

        account.RefreshToken = configuration.GenerateRefreshToken();
        account.RefreshTokenExpiryTime =
            DateTime.UtcNow.AddDays(configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

        await identityContext.SaveChangesAsync();

        return Ok(new SecurityResponse
        {
            Email = account.Email!,
            Token = accessToken,
            RefreshToken = account.RefreshToken
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult<SecurityResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var persistenceAccount = new Account
        {
            Email = request.Email,
            UserName = request.Email
        };

        var result = await userManager.CreateAsync(persistenceAccount, request.Password);

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        if (!result.Succeeded)
        {
            return BadRequest(request);
        }

        // TODO: зачем второй раз получать аккаунт?
        var mapper = new Mapper(new MapperConfiguration(expression => expression.CreateMap<User, Account>()));
        var account = mapper.Map<Account>(await identityContext.Users
            .FirstOrDefaultAsync(account => account.Email == request.Email));

        if (account == null)
        {
            throw new Exception($"Account {request.Email} was not found");
        }

        // TODO: очень ресурсозатратно. доработать.
        var roleList = identityContext.Roles.ToList();
        var role = roleList.FirstOrDefault(role => role.Name == "");

        if (role?.Name == null)
        {
            throw new Exception($"Role for {request.Email} was not found");
        }

        await userManager.AddToRoleAsync(account, role.Name);

        return await Authenticate(new LoginRequest
        {
            Email = request.Email,
            Password = request.Password
        });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenPair? tokenPair)
    {
        if (tokenPair is null)
        {
            return BadRequest("Invalid client request");
        }

        var accessToken = tokenPair.AccessToken;
        var refreshToken = tokenPair.RefreshToken;
        var principal = configuration.GetPrincipalFromExpiredToken(accessToken);

        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        // TODO: это не выглядит безопасным.
        var account = await userManager.FindByNameAsync(principal.Identity!.Name!);

        if (account == null || account.RefreshToken != refreshToken ||
            account.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = configuration.CreateToken(principal.Claims.ToList());
        var newRefreshToken = configuration.GenerateRefreshToken();

        account.RefreshToken = newRefreshToken;

        await userManager.UpdateAsync(account);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost]
    [Route("revoke/{accountName}")]
    public async Task<IActionResult> Revoke(string accountName)
    {
        var account = await userManager.FindByNameAsync(accountName);

        if (account == null)
        {
            return BadRequest("Invalid account name");
        }

        account.RefreshToken = null;

        await userManager.UpdateAsync(account);

        return Ok();
    }

    [Authorize]
    [HttpPost]
    [Route("revoke-all")]
    public async Task<IActionResult> RevokeAll()
    {
        var accountList = userManager.Users.ToList();

        foreach (var account in accountList)
        {
            account.RefreshToken = null;

            // TODO: очень ресурсозатратно. доработать.
            await userManager.UpdateAsync(account);
        }

        return Ok();
    }
}