using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Common;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Common.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Course" />.
/// </summary>
[Route("course")]
[ApiController]
public class CourseController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Контекст данных.</param>
    public CourseController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Контекст данных.
    /// </summary>
    private DataEntityService DataEntityService { get; }

    /// <summary>
    ///     Получить список курсов.
    ///     Если идентификатор не указан, возвращается список со всеми курсами.
    ///     Иначе возвращается список с одним курсом, либо пустой список.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Результат операции со списком курсов.</returns>
    [HttpGet]
    public ActionResult<List<Course>> Get([FromQuery] int id)
    {
        return DataEntityService.HandleGet(DataEntityService.DataContext.Courses, id);
    }

    /// <summary>
    ///     Сохранить курсы.
    /// </summary>
    /// <param name="entities">Список курсов.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Course> entities)
    {
        var status = DataEntityService.HandlePost(DataEntityService.DataContext.Courses, entities);

        if (!status)
        {
            return BadRequest("No courses was saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить курсы.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = DataEntityService.HandleDelete(DataEntityService.DataContext.Courses, ids);

        if (!status)
        {
            return BadRequest("No courses was deleted!");
        }

        return Ok();
    }
}