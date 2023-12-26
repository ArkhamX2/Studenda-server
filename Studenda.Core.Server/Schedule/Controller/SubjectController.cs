using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Schedule;
using Studenda.Core.Server.Common.Service;

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
    /// <param name="dataEntityService">Контекст данных.</param>
    public SubjectController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Контекст данных.
    /// </summary>
    private DataEntityService DataEntityService { get; }

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
        return DataEntityService.HandleGet(DataEntityService.DataContext.Subjects, id);
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору группы.
    /// </summary>
    /// <param name="groupId">Идентификатор группы.</param>
    /// <param name="weekType">Тип недели.</param>
    /// <returns>Результат операции со списком статичных занятий.</returns>
    [HttpGet]
    [Route("group")]
    public ActionResult<List<Subject>> GetByGroup([FromQuery] int groupId, [FromQuery] int weekType)
    {
        return DataEntityService.DataContext.Subjects
            .Where(subject => subject.Group.Id == groupId && subject.WeekType.Index == weekType)
            .OrderBy(subject => subject.DayPosition.Index)
            .ThenBy(subject => subject.SubjectPosition.Index)
            .ToList();
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <param name="weekType">Тип недели.</param>
    /// <returns>Результат операции со списком статичных занятий.</returns>
    [HttpGet]
    [Route("teacher")]
    public ActionResult<List<Subject>> GetByTeacher([FromQuery] int teacherId, [FromQuery] int weekType)
    {
        return DataEntityService.DataContext.Subjects
            .Where(subject => subject.User!.Id == teacherId && subject.WeekType.Index == weekType)
            .OrderBy(subject => subject.DayPosition.Index)
            .ThenBy(subject => subject.SubjectPosition.Index)
            .ToList();
    }

    /// <summary>
    ///     Сохранить статичные занятия.
    /// </summary>
    /// <param name="entities">Список статичных занятий.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Subject> entities)
    {
        var status = DataEntityService.HandlePost(DataEntityService.DataContext.Subjects, entities);

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
        var status = DataEntityService.HandleDelete(DataEntityService.DataContext.Subjects, ids);

        if (!status)
        {
            return BadRequest("No subjects was deleted!");
        }

        return Ok();
    }
}