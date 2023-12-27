using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule;
using Studenda.Core.Server.Schedule.Service;

namespace Studenda.Core.Server.Schedule.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Subject" />.
/// </summary>
[Route("subject")]
[ApiController]
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
    ///     Получить список статичных занятий.
    ///     Если идентификатор не указан, возвращается список со всеми статичными занятиями.
    ///     Иначе возвращается список с одним статичным занятием, либо пустой список.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Результат операции со списком статичных занятий.</returns>
    [HttpGet]
    public ActionResult<List<Subject>> Get([FromQuery] int id)
    {
        return SubjectService.Get(SubjectService.DataContext.Subjects, id);
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору группы.
    /// </summary>
    /// <param name="groupId">Идентификатор группы.</param>
    /// <param name="weekTypeId">Тип недели.</param>
    /// <returns>Результат операции со списком статичных занятий.</returns>
    [HttpGet]
    [Route("group")]
    public ActionResult<List<Subject>> GetByGroup([FromQuery] int groupId, [FromQuery] int weekTypeId)
    {
        return SubjectService.GetSubjectByGroup(groupId, weekTypeId);
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="weekTypeId">Тип недели.</param>
    /// <returns>Результат операции со списком статичных занятий.</returns>
    [HttpGet]
    [Route("user")]
    public ActionResult<List<Subject>> GetByUser([FromQuery] int userId, [FromQuery] int weekTypeId)
    {
        return SubjectService.GetSubjectByUser(userId, weekTypeId);
    }

    /// <summary>
    ///     Сохранить статичные занятия.
    /// </summary>
    /// <param name="entities">Список статичных занятий.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Subject> entities)
    {
        var status = SubjectService.Post(SubjectService.DataContext.Subjects, entities);

        if (!status)
        {
            return BadRequest("No subjects was saved!");
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
        var status = SubjectService.Delete(SubjectService.DataContext.Subjects, ids);

        if (!status)
        {
            return BadRequest("No subjects was deleted!");
        }

        return Ok();
    }
}