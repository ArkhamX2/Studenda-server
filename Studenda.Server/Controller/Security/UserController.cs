using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Server.Model.Security;

namespace Studenda.Server.Controller.Security;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="User" />.
/// </summary>
[Route("api/security/user")]
[ApiController]
public class UserController : ControllerBase
{
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
        return new List<User>();
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
        return Ok();
    }
}