using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Common;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Common.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Course" />.
/// </summary>
[Route("api/course")]
[ApiController]
public class CourseController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    public CourseController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Сервис моделей.
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
    public ActionResult<List<Course>> Get([FromBody] List<int> ids)
    {
        return DataEntityService.Get(DataEntityService.DataContext.Courses, ids);
    }

    /// <summary>
    ///     Сохранить курсы.
    /// </summary>
    /// <param name="entities">Список курсов.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Course> entities)
    {
        var status = DataEntityService.Set(DataEntityService.DataContext.Courses, entities);

        if (!status)
        {
            return BadRequest("No courses were saved!");
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
        var status = DataEntityService.Remove(DataEntityService.DataContext.Courses, ids);

        if (!status)
        {
            return BadRequest("No courses were deleted!");
        }

        return Ok();
    }
}