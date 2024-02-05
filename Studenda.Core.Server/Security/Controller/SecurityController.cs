using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data;
using Studenda.Core.Data.Util;
using Studenda.Core.Model.Security;
using Studenda.Core.Server.Security.Data;
using Studenda.Core.Server.Security.Data.Transfer;
using Studenda.Core.Server.Security.Service.Token;

namespace Studenda.Core.Server.Security.Controller;

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
    ITokenService tokenService,
    UserManager<Account> userManager,
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
    private ITokenService TokenService { get; } = tokenService;

    /// <summary>
    ///     Менеджер работы с пользователями.
    /// </summary>
    private UserManager<Account> UserManager { get; } = userManager;

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

        var identityAccount = await UserManager.FindByEmailAsync(request.Email);

        if (identityAccount is null)
        {
            return Unauthorized();
        }

        if (!await UserManager.CheckPasswordAsync(identityAccount, request.Password))
        {
            return Unauthorized();
        }

        var user = await DataContext.Users
            .Include(user => user.Role)
            .ThenInclude(role => role.RolePermissionLinks)
            .ThenInclude(link => link.Permission)
            .FirstOrDefaultAsync(user => user.IdentityId == identityAccount.Id);

        if (user is null)
        {
            return Unauthorized();
        }

        var identityRoles = await IdentityContext.UserRoles
            .Where(userRole => userRole.UserId == identityAccount.Id)
            .Join(IdentityContext.Roles,
                userRole => userRole.RoleId,
                role => role.Id,
                (userRole, role) => role)
            .ToListAsync();

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            User = user,
            Token = TokenService.CreateToken(identityAccount, identityRoles)
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

        var role = await DataContext.Roles.FirstOrDefaultAsync(role => role.Name == request.RoleName);

        if (role?.Name is null)
        {
            throw new Exception($"Role '{request.RoleName}' does not exists!");
        }

        var identityAccount = new Account
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await UserManager.CreateAsync(identityAccount, request.Password);

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

        var user = await DataContext.Users
            .Include(user => user.Role)
            .ThenInclude(role => role.RolePermissionLinks)
            .ThenInclude(link => link.Permission)
            .FirstOrDefaultAsync(user => user.IdentityId == identityAccount.Id);

        if (user is null)
        {
            return Unauthorized();
        }

        var identityRoles = await IdentityContext.UserRoles
            .Where(userRole => userRole.UserId == identityAccount.Id)
            .Join(IdentityContext.Roles,
                userRole => userRole.RoleId,
                role => role.Id,
                (userRole, role) => role)
            .ToListAsync();

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            User = user,
            Token = TokenService.CreateToken(identityAccount, identityRoles)
        }));
    }
}