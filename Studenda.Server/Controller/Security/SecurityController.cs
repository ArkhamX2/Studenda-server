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
using Studenda.Server.Service;
using Studenda.Server.Service.Security;

namespace Studenda.Server.Controller.Security;

/// <summary>
///     Контроллер авторизации аккаунтов.
/// </summary>
/// <param name="identityContext">Сессия работы с базой данных безопасности.</param>
/// <param name="tokenService">Сервис работы с токенами.</param>
/// <param name="accountService">Сервис работы с аккаунтами.</param>
/// <param name="securityService">Сервис работы с безопасностью.</param>
/// <param name="userManager">Менеджер работы с пользователями.</param>
[Route("api/security")]
[ApiController]
public class SecurityController(
    IdentityContext identityContext,
    TokenService tokenService,
    AccountService accountService,
    SecurityService securityService,
    UserManager<IdentityUser> userManager
) : ControllerBase
{
    private IdentityContext IdentityContext { get; } = identityContext;
    private TokenService TokenService { get; } = tokenService;
    private AccountService AccountService { get; } = accountService;
    private SecurityService SecurityService { get; } = securityService;
    private UserManager<IdentityUser> UserManager { get; } = userManager;

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
    ///     Авторизовать пользователя.
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

        var accounts = await SecurityService.GetAccountsByUser([identityUser]);

        if (accounts.Count <= 0)
        {
            return Unauthorized();
        }

        var identityRoles = await SecurityService.GetUserRoles(identityUser);

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            Account = accounts.First(),
            Token = TokenService.CreateNewToken(identityUser, identityRoles)
        }));
    }

    /// <summary>
    ///     Зарегистрировать нового пользователя.
    /// </summary>
    /// <param name="request">Тело запроса регистрации.</param>
    /// <returns>Тело ответа модуля безопасности.</returns>
    /// <exception cref="Exception">При неудачной попытке создания пользователя.</exception>
    [HttpPost("register")]
    public async Task<ActionResult<SecurityResponse>> RegisterNewUser([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        // TODO: Валидация по request.Account.GroupId.

        if (!SecurityService.CanRegisterWithRoles(request.RoleNames))
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

        await SecurityService.GrantRoleToUser([identityUser], request.RoleNames);
        await AccountService.Set(AccountService.DataContext.Accounts,
        [
            new Account
            {
                IdentityId = identityUser.Id,
                Name = request.Account?.Name,
                Surname = request.Account?.Surname,
                Patronymic = request.Account?.Patronymic,
                GroupId = request.Account?.GroupId
            }
        ]);

        var accounts = await SecurityService.GetAccountsByUser([identityUser]);

        if (accounts.Count <= 0)
        {
            return Unauthorized();
        }

        var identityRoles = await SecurityService.GetUserRoles(identityUser);

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            Account = accounts.First(),
            Token = TokenService.CreateNewToken(identityUser, identityRoles)
        }));
    }

    /// <summary>
    ///     Создать нового пользователя.
    /// </summary>
    /// <param name="request">Тело запроса регистрации.</param>
    /// <returns>Тело ответа модуля безопасности.</returns>
    /// <exception cref="Exception">При неудачной попытке создания пользователя.</exception>
    [Authorize(Policy = AdminRoleAuthorizationRequirement.AuthorizationPolicyCode)]
    [HttpPost("user")]
    public async Task<ActionResult<SecurityResponse>> CreateNewUser([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        // TODO: Валидация по request.Account.GroupId.

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
            throw new Exception("Error while creating new identity user!");
        }

        await SecurityService.GrantRoleToUser([identityUser], request.RoleNames);
        await AccountService.Set(AccountService.DataContext.Accounts,
        [
            new Account
            {
                IdentityId = identityUser.Id,
                Name = request.Account?.Name,
                Surname = request.Account?.Surname,
                Patronymic = request.Account?.Patronymic,
                GroupId = request.Account?.GroupId
            }
        ]);

        var accounts = await SecurityService.GetAccountsByUser([identityUser]);

        if (accounts.Count <= 0)
        {
            throw new Exception("Error while creating new user account!");
        }

        var identityRoles = await SecurityService.GetUserRoles(identityUser);

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            Account = accounts.First(),
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

        var accounts = await SecurityService.GetAccountsByUser([identityUser]);

        if (accounts.Count <= 0)
        {
            return Unauthorized();
        }

        var identityRoles = await SecurityService.GetUserRoles(identityUser);

        return Ok(DataSerializer.Serialize(new SecurityResponse
        {
            Account = accounts.First(),
            Token = TokenService.CreateNewToken(identityUser, identityRoles)
        }));
    }
}