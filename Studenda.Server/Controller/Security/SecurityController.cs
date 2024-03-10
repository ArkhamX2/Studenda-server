using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studenda.Server.Configuration.Static;
using Studenda.Server.Data;
using Studenda.Server.Data.Transfer.Security;
using Studenda.Server.Data.Util;
using Studenda.Server.Middleware.Security.Requirement;
using Studenda.Server.Model.Common;
using Studenda.Server.Service.Security;
using ConfigurationManager = Studenda.Server.Configuration.ConfigurationManager;

namespace Studenda.Server.Controller.Security;

/// <summary>
///     Контроллер авторизации аккаунтов.
/// </summary>
/// <param name="configurationManager">Менеджер конфигурации.</param>
/// <param name="dataContext">Сессия работы с базой данных.</param>
/// <param name="identityContext">Сессия работы с базой данных безопасности.</param>
/// <param name="tokenService">Сервис работы с токенами.</param>
/// <param name="userManager">Менеджер работы с пользователями.</param>
/// <param name="roleManager">Менеджер работы с ролями.</param>
[Route("api/security")]
[ApiController]
public class SecurityController(
    ConfigurationManager configurationManager,
    DataContext dataContext,
    IdentityContext identityContext,
    TokenService tokenService,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager
) : ControllerBase
{
    private ConfigurationManager ConfigurationManager { get; } = configurationManager;
    private DataContext DataContext { get; } = dataContext;
    private IdentityContext IdentityContext { get; } = identityContext;
    private TokenService TokenService { get; } = tokenService;
    private UserManager<IdentityUser> UserManager { get; } = userManager;
    private RoleManager<IdentityRole> RoleManager { get; } = roleManager;

    /// <summary>
    ///     Получить базовые параметры.
    /// </summary>
    /// <returns>Результат с базовыми параметрами.</returns>
    [HttpGet]
    public ActionResult<List<Account>> Get()
    {
        return Ok(new HandshakeResponse
        {
            StudentRoleName = IdentityRoleConfiguration.StudentRoleName,
            LeaderRoleName = IdentityRoleConfiguration.LeaderRoleName,
            TeacherRoleName = IdentityRoleConfiguration.TeacherRoleName,
            AdminRoleName = IdentityRoleConfiguration.AdminRoleName,
            CoordinatedUniversalTime = DateTime.UtcNow
        });
    }

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

        if (!CanRegisterWithRoles(request.RoleNames))
        {
            return BadRequest("Incorrect roles!");
        }

        var result = await UserManager.CreateAsync(
            new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            },
            request.Password);

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
            throw new Exception("Internal error!");
        }

        foreach (var roleName in request.RoleNames)
        {
            await RoleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
            });
        }

        await UserManager.AddToRolesAsync(identityUser, request.RoleNames);
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

    /// <summary>
    ///     Выпустить новый токен для текущего авторизованного пользователя.
    /// </summary>
    /// <returns>Тело ответа модуля безопасности.</returns>
    [Authorize(Policy = StudentRoleAuthorizationRequirement.AuthorizationPolicyCode)]
    [HttpPost("token")]
    public async Task<IActionResult> RefreshToken()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        var identityUser = await IdentityContext.Users
            .FirstOrDefaultAsync(identityUser => identityUser.UserName == User.Identity.Name);

        if (identityUser is null)
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
                innerRole => innerRole.Id,
                (userRole, innerRole) => innerRole)
            .ToListAsync();

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            Account = account,
            Token = TokenService.CreateNewToken(identityUser, identityRoles)
        }));
    }

    /// <summary>
    ///     Проверить возможность регистрации с указанными названиями ролей.
    /// </summary>
    /// <param name="roleNames">Имена ролей.</param>
    /// <returns>Статус проверки.</returns>
    private bool CanRegisterWithRoles(List<string> roleNames)
    {
        foreach (var roleName in roleNames)
        {
            if (!IdentityRoleConfiguration.GetRoleNames().Contains(roleName))
            {
                return false;
            }

            if (!ConfigurationManager.IdentityConfiguration.GetRoleCanRegister(roleName))
            {
                return false;
            }
        }

        return true;
    }
}