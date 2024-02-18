using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Journal;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Journal.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Assessment" />.
/// </summary>
/// <param name="dataEntityService">Сервис моделей.</param>
[Route("api/journal/assessment")]
[ApiController]
public class AssessmentController(DataEntityService dataEntityService) : ControllerBase
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
    public async Task<ActionResult<List<Assessment>>> Get([FromQuery] List<int> ids)
    {
        return await DataEntityService.Get(DataEntityService.DataContext.Assessments, ids);
    }

    /// <summary>
    ///     Сохранить оценки.
    /// </summary>
    /// <param name="entities">Список оценок.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Assessment> entities)
    {
        var status = await DataEntityService.Set(DataEntityService.DataContext.Assessments, entities);

        if (!status)
        {
            return BadRequest("No assessments were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить оценки.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        var status = await DataEntityService.Remove(DataEntityService.DataContext.Assessments, ids);

        if (!status)
        {
            return BadRequest("No assessments were deleted!");
        }

        return Ok();
    }
}