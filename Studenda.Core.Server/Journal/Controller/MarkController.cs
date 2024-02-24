using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Journal;
using Studenda.Core.Server.Journal.Service;

namespace Studenda.Core.Server.Journal.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Mark" />.
/// </summary>
/// <param name="markService">Сервис моделей.</param>
[Route("api/journal/mark")]
[ApiController]
public class MarkController(MarkService markService) : ControllerBase
{
    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private MarkService MarkService { get; } = markService;

    /// <summary>
    ///     Получить список оценок.
    ///     Если идентификаторы не указаны, возвращается список со всеми оценками.
    ///     Иначе возвращается список с указанными оценками, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком оценок.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Mark>>> Get([FromQuery] List<int> ids)
    {
        return await MarkService.Get(MarkService.DataContext.Marks, ids);
    }

    /// <summary>
    ///     Получить список оценок по идентификаторам заданий.
    /// </summary>
    /// <param name="taskIds">Идентификаторы заданий.</param>
    /// <returns>Результат операции со списком оценок.</returns>
    [HttpGet]
    [Route("task")]
    public async Task<ActionResult<List<Mark>>> GetByTask([FromQuery] List<int> taskIds)
    {
        return await MarkService.GetByTask(taskIds);
    }

    /// <summary>
    ///     Сохранить оценки.
    /// </summary>
    /// <param name="entities">Список оценок.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Mark> entities)
    {
        var status = await MarkService.Set(MarkService.DataContext.Marks, entities);

        if (!status)
        {
            return BadRequest("No marks were saved!");
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
        var status = await MarkService.Remove(MarkService.DataContext.Marks, ids);

        if (!status)
        {
            return BadRequest("No marks were deleted!");
        }

        return Ok();
    }
}