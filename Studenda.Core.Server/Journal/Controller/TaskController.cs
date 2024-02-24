using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Server.Journal.Service;
using Task = Studenda.Core.Model.Journal.Task;

namespace Studenda.Core.Server.Journal.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Task" />.
/// </summary>
/// <param name="taskService">Сервис моделей.</param>
[Route("api/journal/task")]
[ApiController]
public class TaskController(TaskService taskService) : ControllerBase
{
    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private TaskService TaskService { get; } = taskService;

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
        return await TaskService.Get(TaskService.DataContext.Tasks, ids);
    }

    /// <summary>
    ///     Получить список заданий по идентификатору издателя.
    /// </summary>
    /// <param name="issuerUserId">Идентификатор издателя.</param>
    /// <param name="groupIds">Идентификаторы групп.</param>
    /// <param name="disciplineId">Идентификатор дисциплины или null.</param>
    /// <param name="subjectTypeId">Идентификатор типа занятия или null.</param>
    /// <param name="academicYear">Учебный год или null.</param>
    /// <returns>Результат операции со списком заданий.</returns>
    [HttpGet]
    [Route("issuer")]
    public async Task<ActionResult<List<Task>>> GetByIssuer(
        [FromQuery] int issuerUserId,
        [FromQuery] List<int> groupIds,
        [FromQuery] int? disciplineId,
        [FromQuery] int? subjectTypeId,
        [FromQuery] int? academicYear
    ) {
        return await TaskService.GetByIssuer(
            issuerUserId,
            groupIds,
            disciplineId,
            subjectTypeId,
            academicYear
        );
    }

    /// <summary>
    ///     Получить список заданий по идентификаторам исполнителей.
    /// </summary>
    /// <param name="assigneeUserIds">Идентификаторы исполнителей.</param>
    /// <param name="disciplineId">Идентификатор дисциплины или null.</param>
    /// <param name="subjectTypeId">Идентификатор типа занятия или null.</param>
    /// <param name="academicYear">Учебный год или null.</param>
    /// <returns>Результат операции со списком заданий.</returns>
    [HttpGet]
    [Route("assignee")]
    public async Task<ActionResult<List<Task>>> GetByAssignee(
        [FromQuery] List<int> assigneeUserIds,
        [FromQuery] int? disciplineId,
        [FromQuery] int? subjectTypeId,
        [FromQuery] int? academicYear
    ) {
        return await TaskService.GetByAssignee(
            assigneeUserIds,
            disciplineId,
            subjectTypeId,
            academicYear
        );
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
        var status = await TaskService.Set(TaskService.DataContext.Tasks, entities);

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
        var status = await TaskService.Remove(TaskService.DataContext.Tasks, ids);

        if (!status)
        {
            return BadRequest("No tasks were deleted!");
        }

        return Ok();
    }
}