using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Journal;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Journal.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Absence" />.
/// </summary>
/// <param name="dataEntityService">Сервис моделей.</param>
[Route("api/journal/absence")]
[ApiController]
public class AbsenceController(DataEntityService dataEntityService) : ControllerBase
{
    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; } = dataEntityService;

    /// <summary>
    ///     Получить список прогулов.
    ///     Если идентификаторы не указаны, возвращается список со всеми прогулами.
    ///     Иначе возвращается список с указанными прогулами, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком прогулов.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Absence>>> Get([FromQuery] List<int> ids)
    {
        return await DataEntityService.Get(DataEntityService.DataContext.Absences, ids);
    }

    /// <summary>
    ///     Сохранить прогулы.
    /// </summary>
    /// <param name="entities">Список прогулов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Absence> entities)
    {
        var status = await DataEntityService.Set(DataEntityService.DataContext.Absences, entities);

        if (!status)
        {
            return BadRequest("No absences were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить прогулы.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        var status = await DataEntityService.Remove(DataEntityService.DataContext.Absences, ids);

        if (!status)
        {
            return BadRequest("No absences were deleted!");
        }

        return Ok();
    }
}