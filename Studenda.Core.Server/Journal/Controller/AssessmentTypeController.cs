using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Journal.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Journal.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="AssessmentType" />.
/// </summary>
/// <param name="dataEntityService">Сервис моделей.</param>
[Route("api/journal/assessment-type")]
[ApiController]
public class AssessmentTypeController(DataEntityService dataEntityService) : ControllerBase
{
    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; } = dataEntityService;

    /// <summary>
    ///     Получить список типов оценивания.
    ///     Если идентификаторы не указаны, возвращается список со всеми типами.
    ///     Иначе возвращается список с указанными типами, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком типов оценивания.</returns>
    [HttpGet]
    public async Task<ActionResult<List<AssessmentType>>> Get([FromQuery] List<int> ids)
    {
        return await DataEntityService.Get(DataEntityService.DataContext.AssessmentTypes, ids);
    }

    /// <summary>
    ///     Сохранить типы оценивания.
    /// </summary>
    /// <param name="entities">Список типов оценивания.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<AssessmentType> entities)
    {
        var status = await DataEntityService.Set(DataEntityService.DataContext.AssessmentTypes, entities);

        if (!status)
        {
            return BadRequest("No assessment types were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить типы оценивания.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        var status = await DataEntityService.Remove(DataEntityService.DataContext.AssessmentTypes, ids);

        if (!status)
        {
            return BadRequest("No assessment types were deleted!");
        }

        return Ok();
    }
}