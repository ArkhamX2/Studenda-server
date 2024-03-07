﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studenda.Server.Data;
using Studenda.Server.Data.Transfer.Security;
using Studenda.Server.Data.Util;
using Studenda.Server.Model.Common;
using Studenda.Server.Service.Security;

namespace Studenda.Server.Controller.Security;

/// <summary>
///     Контроллер авторизации аккаунтов.
/// </summary>
/// <param name="dataContext">Сессия работы с базой данных.</param>
/// <param name="identityContext">Сессия работы с базой данных безопасности.</param>
/// <param name="tokenService">Сервис работы с токенами.</param>
/// <param name="userManager">Менеджер работы с пользователями.</param>
/// <param name="roleManager">Менеджер работы с ролями.</param>
[Route("api/security")]
[ApiController]
public class SecurityController(
    DataContext dataContext,
    IdentityContext identityContext,
    TokenService tokenService,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager) : ControllerBase
{
    /// <summary>
    ///     Сессия работы с базой данных.
    /// </summary>
    private DataContext DataContext { get; } = dataContext;

    /// <summary>
    ///     Сессия работы с базой данных безопасности.
    /// </summary>
    private IdentityContext IdentityContext { get; } = identityContext;

    /// <summary>
    ///     Сервис работы с токенами.
    /// </summary>
    private TokenService TokenService { get; } = tokenService;

    /// <summary>
    ///     Менеджер работы с пользователями.
    /// </summary>
    private UserManager<IdentityUser> UserManager { get; } = userManager;

    /// <summary>
    ///     Менеджер работы с ролями.
    /// </summary>
    private RoleManager<IdentityRole> RoleManager { get; } = roleManager;

    /// <summary>
    ///     Авторизация пользователя.
    /// </summary>
    /// <param name="request">Тело запроса авторизации.</param>
    /// <returns>Тело ответа модуля безопасности.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<SecurityResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var identityUser = await UserManager.FindByEmailAsync(request.Email);

        if (identityUser is null)
        {
            return Unauthorized();
        }

        if (!await UserManager.CheckPasswordAsync(identityUser, request.Password))
        {
            return Unauthorized();
        }

        var account = await DataContext.Accounts.FirstOrDefaultAsync(account => account.IdentityId == identityUser.Id);

        if (account is null)
        {
            return Unauthorized();
        }

        var identityRoles = await IdentityContext.UserRoles
            .Where(userRole => userRole.UserId == identityUser.Id)
            .Join(IdentityContext.Roles,
                userRole => userRole.RoleId,
                role => role.Id,
                (userRole, role) => role)
            .ToListAsync();

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            Account = account,
            Token = TokenService.CreateNewToken(identityUser, identityRoles)
        }));
    }

    /// <summary>
    ///     Регистрация пользователя.
    /// </summary>
    /// <param name="request">Тело запроса регистрации.</param>
    /// <returns>Тело ответа модуля безопасности.</returns>
    /// <exception cref="Exception">При неудачной попытке создания пользователя.</exception>
    [HttpPost("register")]
    public async Task<ActionResult<SecurityResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        // TODO: Валидация роли.

        var internalUser = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await UserManager.CreateAsync(internalUser, request.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        var identityUser = await IdentityContext.Users
            .FirstOrDefaultAsync(identityUser => identityUser.Email == request.Email);

        if (identityUser is null)
        {
            throw new Exception("Internal error! Please try again.");
        }

        var identityRole = new IdentityRole
        {
            Name = request.RoleName
        };

        await RoleManager.CreateAsync(identityRole);
        await UserManager.AddToRoleAsync(identityUser, request.RoleName);
        await DataContext.Accounts.AddAsync(new Account
        {
            IdentityId = identityUser.Id
        });

        await DataContext.SaveChangesAsync();

        var account = await DataContext.Accounts.FirstOrDefaultAsync(account => account.IdentityId == identityUser.Id);

        if (account is null)
        {
            return Unauthorized();
        }

        var identityRoles = await IdentityContext.UserRoles
            .Where(userRole => userRole.UserId == identityUser.Id)
            .Join(IdentityContext.Roles,
                userRole => userRole.RoleId,
                innerRole => innerRole.Id,
                (userRole, innerRole) => innerRole)
            .ToListAsync();

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            Account = account,
            Token = TokenService.CreateNewToken(identityUser, identityRoles)
        }));
    }
}