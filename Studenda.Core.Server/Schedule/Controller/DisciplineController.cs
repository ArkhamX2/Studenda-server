using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Discipline" />.
/// </summary>
[Route("api/schedule/discipline")]
[ApiController]
public class DisciplineController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    public DisciplineController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; }

    /// <summary>
    ///     Получить список всех дисциплин.
    /// </summary>
    /// <returns>Результат операции со списком дисциплин.</returns>
    [HttpGet("all")]
    public ActionResult<List<Discipline>> GetAll()
    {
        return DataEntityService.Get(DataEntityService.DataContext.Disciplines, new List<int>());
    }

    /// <summary>
    ///     Получить список дисциплин.
    ///     Если идентификаторы не указаны, возвращается список со всеми дисциплинами.
    ///     Иначе возвращается список с указанными дисциплинами, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком дисциплин.</returns>
    [HttpGet]
    public ActionResult<List<Discipline>> Get([FromQuery] List<int> ids)
    {
        return DataEntityService.Get(DataEntityService.DataContext.Disciplines, ids);
    }

    /// <summary>
    ///     Сохранить дисциплины.
    /// </summary>
    /// <param name="entities">Список дисциплин.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Discipline> entities)
    {
        var status = DataEntityService.Set(DataEntityService.DataContext.Disciplines, entities);

        if (!status)
        {
            return BadRequest("No disciplines were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить дисциплины.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = DataEntityService.Remove(DataEntityService.DataContext.Disciplines, ids);

        if (!status)
        {
            return BadRequest("No disciplines were deleted!");
        }

        return Ok();
    }
}