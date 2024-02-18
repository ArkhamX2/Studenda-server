using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Server.Common.Service;
using Task = Studenda.Core.Model.Journal.Task;

namespace Studenda.Core.Server.Journal.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Task" />.
/// </summary>
/// <param name="dataEntityService">Сервис моделей.</param>
[Route("api/journal/task")]
[ApiController]
public class TaskController(DataEntityService dataEntityService) : ControllerBase
{
    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; } = dataEntityService;

    /// <summary>
    ///     Получить список заданий.
    ///     Если идентификаторы не указаны, возвращается список со всеми заданиями.
    ///     Иначе возвращается список с указанными заданиями, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком заданий.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Task>>> Get([FromQuery] List<int> ids)
    {
        return await DataEntityService.Get(DataEntityService.DataContext.Tasks, ids);
    }

    /// <summary>
    ///     Сохранить задания.
    /// </summary>
    /// <param name="entities">Список заданий.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Task> entities)
    {
        var status = await DataEntityService.Set(DataEntityService.DataContext.Tasks, entities);

        if (!status)
        {
            return BadRequest("No tasks were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить задания.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        var status = await DataEntityService.Remove(DataEntityService.DataContext.Tasks, ids);

        if (!status)
        {
            return BadRequest("No tasks were deleted!");
        }

        return Ok();
    }
}