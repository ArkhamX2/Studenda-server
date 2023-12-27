using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="WeekType" />.
/// </summary>
[Route("week-type")]
[ApiController]
public class WeekTypeController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    public WeekTypeController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; }

    /// <summary>
    ///     Получить список типов недель.
    ///     Если идентификатор не указан, возвращается список со всеми типами недель.
    ///     Иначе возвращается список с одним типом недели, либо пустой список.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Результат операции со списком типов недель.</returns>
    [HttpGet]
    public ActionResult<List<WeekType>> Get([FromQuery] int id)
    {
        return DataEntityService.Get(DataEntityService.DataContext.WeekTypes, id);
    }

    /// <summary>
    ///     Сохранить типы недель.
    /// </summary>
    /// <param name="entities">Список типов недель.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<WeekType> entities)
    {
        var status = DataEntityService.Post(DataEntityService.DataContext.WeekTypes, entities);

        if (!status)
        {
            return BadRequest("No week types was saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить типы недель.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = DataEntityService.Delete(DataEntityService.DataContext.WeekTypes, ids);

        if (!status)
        {
            return BadRequest("No week types was deleted!");
        }

        return Ok();
    }
}