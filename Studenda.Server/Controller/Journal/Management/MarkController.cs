using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Server.Middleware.Security.Requirement;
using Studenda.Server.Model.Journal.Management;
using Studenda.Server.Service;

namespace Studenda.Server.Controller.Journal.Management;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Mark" />.
/// </summary>
/// <param name="dataEntityService">Сервис моделей.</param>
[Route("api/journal/mark")]
[ApiController]
public class MarkController(DataEntityService dataEntityService) : ControllerBase
{
    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; } = dataEntityService;

    /// <summary>
    ///     Получить список оценок.
    ///     Если идентификаторы не указаны, возвращается список со всеми оценками.
    ///     Иначе возвращается список с указанными оценками, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком оценок.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Mark>>> Get([FromQuery] List<int> ids)
    {
        return await DataEntityService.Get(DataEntityService.DataContext.Marks, ids);
    }

    /// <summary>
    ///     Сохранить оценки.
    /// </summary>
    /// <param name="entities">Список оценок.</param>
    /// <returns>Результат операции.</returns>
    [Authorize(Policy = TeacherAuthorizationRequirement.PolicyCode)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Mark> entities)
    {
        var status = await DataEntityService.Set(DataEntityService.DataContext.Marks, entities);

        if (!status)
        {
            return BadRequest("No marks were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить оценки.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize(Policy = TeacherAuthorizationRequirement.PolicyCode)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        var status = await DataEntityService.Remove(DataEntityService.DataContext.Marks, ids);

        if (!status)
        {
            return BadRequest("No marks were deleted!");
        }

        return Ok();
    }
}