using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="DayPosition" />.
/// </summary>
[Route("api/schedule/day-position")]
[ApiController]
public class DayPositionController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    public DayPositionController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; }

    /// <summary>
    ///     Получить список позиций учебного дня.
    ///     Если идентификаторы не указаны, возвращается список со всеми позициями.
    ///     Иначе возвращается список с указанными позициями, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком позиций.</returns>
    [HttpGet]
    public ActionResult<List<DayPosition>> Get([FromQuery] List<int> ids)
    {
        return DataEntityService.Get(DataEntityService.DataContext.DayPositions, ids);
    }

    /// <summary>
    ///     Сохранить позиции учебного дня.
    /// </summary>
    /// <param name="entities">Список позиций.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<DayPosition> entities)
    {
        var status = DataEntityService.Set(DataEntityService.DataContext.DayPositions, entities);

        if (!status)
        {
            return BadRequest("No day positions were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить позиции учебного дня.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = DataEntityService.Remove(DataEntityService.DataContext.DayPositions, ids);

        if (!status)
        {
            return BadRequest("No day positions were deleted!");
        }

        return Ok();
    }
}