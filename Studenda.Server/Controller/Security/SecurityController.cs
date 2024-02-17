using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Studenda.Server.Data;
using Studenda.Server.Data.Transfer.Security;
using Studenda.Server.Service.Security;

namespace Studenda.Server.Controller.Security;

/// <summary>
///     Контроллер авторизации аккаунтов.
/// </summary>
/// <param name="dataContext">Сессия работы с базой данных.</param>
/// <param name="tokenService">Сервис работы с токенами.</param>
/// <param name="userManager">Менеджер работы с пользователями.</param>
/// <param name="roleManager">Менеджер работы с ролями.</param>
[Route("api/security")]
[ApiController]
public class SecurityController(
    DataContext dataContext,
    TokenService tokenService,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager) : ControllerBase
{
    /// <summary>
    ///     Сессия работы с базой данных.
    /// </summary>
    private DataContext DataContext { get; } = dataContext;

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
        return Ok();
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
        return Ok();
    }
}