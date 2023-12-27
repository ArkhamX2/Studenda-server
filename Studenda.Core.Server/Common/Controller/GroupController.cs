using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Common;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Common.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Group" />.
/// </summary>
[Route("group")]
[ApiController]
public class GroupController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    public GroupController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; }

    /// <summary>
    ///     Получить список групп.
    ///     Если идентификатор не указан, возвращается список со всеми группами.
    ///     Иначе возвращается список с одной группой, либо пустой список.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Результат операции со списком групп.</returns>
    [HttpGet]
    public ActionResult<List<Group>> Get([FromQuery] int id)
    {
        return DataEntityService.Get(DataEntityService.DataContext.Groups, id);
    }

    /// <summary>
    ///     Сохранить группы.
    /// </summary>
    /// <param name="entities">Список групп.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Group> entities)
    {
        var status = DataEntityService.Post(DataEntityService.DataContext.Groups, entities);

        if (!status)
        {
            return BadRequest("No groups was saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить группы.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = DataEntityService.Delete(DataEntityService.DataContext.Groups, ids);

        if (!status)
        {
            return BadRequest("No groups was deleted!");
        }

        return Ok();
    }
}