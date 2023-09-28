﻿using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data;
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
    public AccountController(
        IConfiguration configuration,
        DataContext dataContext,
        IdentityContext identityContext,
        ITokenService tokenService,
        UserManager<Account> userManager)
    {
        Configuration = configuration;
        DataContext = dataContext;
        IdentityContext = identityContext;
        TokenService = tokenService;
        UserManager = userManager;
    }

    private IConfiguration Configuration { get; }
    private DataContext DataContext { get; }
    private IdentityContext IdentityContext { get; }
    private ITokenService TokenService { get; }
    private UserManager<Account> UserManager { get; }

    [HttpPost("login")]
    public async Task<ActionResult<SecurityResponse>> Authenticate([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var persistenceAccount = await UserManager.FindByEmailAsync(request.Email);

        if (persistenceAccount == null)
        {
            return BadRequest("Bad credentials");
        }

        var isValidPassword = await UserManager.CheckPasswordAsync(persistenceAccount, request.Password);

        if (!isValidPassword)
        {
            return BadRequest("Bad credentials");
        }

        // TODO: зачем второй раз получать аккаунт?
        var mapper = new Mapper(new MapperConfiguration(expression => expression.CreateMap<User, Account>()));
        var account = mapper.Map<Account>(IdentityContext
            .Users.FirstOrDefault(account => account.Email == request.Email));

        if (account is null)
        {
            return Unauthorized();
        }

        var roleIds = await IdentityContext.UserRoles
            .Where(r => r.UserId == account.Id)
            .Select(x => x.RoleId)
            .ToListAsync();
        var roles = IdentityContext.Roles.Where(role => roleIds.Contains(role.Id)).ToList();

        var accessToken = TokenService.CreateToken(account, roles);

        account.RefreshToken = Configuration.GenerateRefreshToken();
        account.RefreshTokenExpiryTime =
            DateTime.UtcNow.AddDays(Configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

        await IdentityContext.SaveChangesAsync();

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

        var result = await UserManager.CreateAsync(persistenceAccount, request.Password);

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
        var account = mapper.Map<Account>(await IdentityContext.Users
            .FirstOrDefaultAsync(account => account.Email == request.Email));

        if (account == null)
        {
            throw new Exception($"Account {request.Email} was not found");
        }

        // TODO: очень ресурсозатратно. доработать.
        var roleList = IdentityContext.Roles.ToList();
        var role = roleList.FirstOrDefault(role => role.Name == "Student");

        if (role?.Name == null)
        {
            throw new Exception($"Role for {request.Email} was not found");
        }
        if (account.Email == "Quest")
        {
            await UserManager.AddToRoleAsync(account, "Quest");
        }
        else
        {
            await UserManager.AddToRoleAsync(account, role.Name);
        }

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
    [Route("revoke/{accountName}")]
    public async Task<IActionResult> Revoke(string accountName)
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
    [Route("revoke-all")]
    public async Task<IActionResult> RevokeAll()
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
    [Route("hello")]
    public async Task Test()
    {
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync("<h2>Hello METANIT.COM</h2>");
    }
    [Authorize(Roles ="Quest")]
    [HttpGet]
    [Route("helloQ")]
    public async Task TestQuest()
    {
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync("<h2>Hello Quest</h2>");
    }
    [Authorize(Roles = "Student")]
    [HttpGet]
    [Route("helloS")]
    public async Task TestStudent()
    {
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync("<h2>Hello Student</h2>");
    }
}