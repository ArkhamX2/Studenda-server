using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule;
using Studenda.Core.Server.Schedule.Service;

namespace Studenda.Core.Server.Schedule.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Subject" />.
/// </summary>
[Route("api/schedule/subject")]
[ApiController]
[Authorize]
public class SubjectController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="subjectService">Сервис статичных занятий.</param>
    public SubjectController(SubjectService subjectService)
    {
        SubjectService = subjectService;
    }

    /// <summary>
    ///     Сервис статичных занятий.
    /// </summary>
    private SubjectService SubjectService { get; }

    /// <summary>
    ///     Получить список всех статичных занятий.
    /// </summary>
    /// <returns>Результат операции со списком занятий.</returns>
    [HttpGet("all")]
    public ActionResult<List<Subject>> GetAll()
    {
        return SubjectService.Get(SubjectService.DataContext.Subjects, new List<int>());
    }

    /// <summary>
    ///     Получить список статичных занятий.
    ///     Если идентификаторы не указаны, возвращается список со всеми занятиями.
    ///     Иначе возвращается список с указанными занятиями, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком занятий.</returns>
    [HttpGet]
    public ActionResult<List<Subject>> Get([FromQuery] List<int> ids)
    {
        return SubjectService.Get(SubjectService.DataContext.Subjects, ids);
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору группы.
    /// </summary>
    /// <param name="groupId">Идентификатор группы.</param>
    /// <param name="weekTypeId">Тип недели.</param>
    /// <param name="year">Учебный год.</param>
    /// <returns>Результат операции со списком статичных занятий.</returns>
    [HttpGet]
    [Route("group")]
    public ActionResult<List<Subject>> GetByGroup([FromQuery] int groupId, [FromQuery] int weekTypeIndex, [FromQuery] int year)
    {
        return SubjectService.GetSubjectByGroup(groupId, weekTypeIndex, year);
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="weekTypeId">Тип недели.</param>
    /// <param name="year">Учебный год.</param>
    /// <returns>Результат операции со списком статичных занятий.</returns>
    [HttpGet]
    [Route("user")]
    public ActionResult<List<Subject>> GetByUser([FromQuery] int userId, [FromQuery] int weekTypeIndex, [FromQuery] int year)
    {
        return SubjectService.GetSubjectByUser(userId, weekTypeIndex, year);
    }

    /// <summary>
    ///     Сохранить статичные занятия.
    /// </summary>
    /// <param name="entities">Список статичных занятий.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Subject> entities)
    {
        var status = SubjectService.Set(SubjectService.DataContext.Subjects, entities);

        if (!status)
        {
            return BadRequest("No subjects were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить статичные занятия.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = SubjectService.Remove(SubjectService.DataContext.Subjects, ids);

        if (!status)
        {
            return BadRequest("No subjects were deleted!");
        }

        return Ok();
    }
}