using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Server.Model.Security;
using Studenda.Server.Service.Security;

namespace Studenda.Server.Controller.Security;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="User" />.
/// </summary>
/// <param name="userService">Сервис моделей.</param>
[Route("api/security/user")]
[ApiController]
public class UserController(UserService userService) : ControllerBase
{
    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private UserService UserService { get; } = userService;

    /// <summary>
    ///     Получить список пользователей.
    ///     Если идентификаторы не указаны, возвращается список со всеми пользователями.
    ///     Иначе возвращается список с указанными пользователями, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком пользователей.</returns>
    [HttpGet]
    public async Task<ActionResult<List<User>>> Get([FromQuery] List<int> ids)
    {
        return await UserService.Get(UserService.DataContext.Users, ids);
    }

    /// <summary>
    ///     Получить список пользователей по идентификаторам групп.
    /// </summary>
    /// <param name="groupIds">Идентификаторы групп.</param>
    /// <returns>Результат операции со списком пользователей.</returns>
    public async Task<ActionResult<List<User>>> GetByGroup([FromQuery] List<int> groupIds)
    {
        return await UserService.GetByGroup(groupIds);
    }

    /// <summary>
    ///     Сохранить пользователей.
    /// </summary>
    /// <param name="entities">Список пользователей.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<User> entities)
    {
        var status = await UserService.Set(UserService.DataContext.Users, entities);

        if (!status)
        {
            return BadRequest("No users were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить пользователей.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        var status = await UserService.Remove(UserService.DataContext.Users, ids);

        if (!status)
        {
            return BadRequest("No users were deleted!");
        }

        return Ok();
    }
}