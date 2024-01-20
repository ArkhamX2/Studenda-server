using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="SubjectType" />.
/// </summary>
[Route("api/schedule/subject-type")]
[ApiController]
public class SubjectTypeController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    public SubjectTypeController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; }

    /// <summary>
    ///     Получить список типов занятия.
    ///     Если идентификаторы не указаны, возвращается список со всеми типами.
    ///     Иначе возвращается список с указанными типами, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком типов.</returns>
    [HttpGet]
    public ActionResult<List<SubjectType>> Get([FromQuery] List<int> ids)
    {
        return DataEntityService.Get(DataEntityService.DataContext.SubjectTypes, ids);
    }

    /// <summary>
    ///     Сохранить типы занятия.
    /// </summary>
    /// <param name="entities">Список типов.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<SubjectType> entities)
    {
        var status = DataEntityService.Set(DataEntityService.DataContext.SubjectTypes, entities);

        if (!status)
        {
            return BadRequest("No subject types were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить типы занятия.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = DataEntityService.Remove(DataEntityService.DataContext.SubjectTypes, ids);

        if (!status)
        {
            return BadRequest("No subject types were deleted!");
        }

        return Ok();
    }
}