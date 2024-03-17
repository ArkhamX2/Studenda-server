using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Server.Middleware.Security.Requirement;
using Studenda.Server.Model.Common;
using Studenda.Server.Service;

namespace Studenda.Server.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Role" />.
/// </summary>
/// <param name="roleService">Сервис моделей.</param>
[Route("api/role")]
[ApiController]
public class RoleController(RoleService roleService) : ControllerBase
{
    private RoleService RoleService { get; } = roleService;

    /// <summary>
    ///     Получить список ролей.
    ///     Если идентификаторы не указаны, возвращается список со всеми ролями.
    ///     Иначе возвращается список с указанными ролями, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком ролей.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Role>>> Get([FromQuery] List<int> ids)
    {
        return await RoleService.Get(RoleService.DataContext.Roles, ids);
    }

    /// <summary>
    ///     Сохранить роли.
    /// </summary>
    /// <param name="entities">Список ролей.</param>
    /// <returns>Результат операции.</returns>
    [Authorize(Policy = AdminAuthorizationRequirement.PolicyCode)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Role> entities)
    {
        var status = await RoleService.Set(RoleService.DataContext.Roles, entities);

        if (!status)
        {
            return BadRequest("No roles were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить роли.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize(Policy = AdminAuthorizationRequirement.PolicyCode)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        var status = await RoleService.Remove(RoleService.DataContext.Roles, ids);

        if (!status)
        {
            return BadRequest("No roles were deleted!");
        }

        return Ok();
    }
}