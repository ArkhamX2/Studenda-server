using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Common;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Common.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Group" />.
/// </summary>
[Route("api/group")]
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
        var groups= DataEntityService.Get(DataEntityService.DataContext.Groups, id);
        foreach (var group in groups)
        {
            group.Course=DataEntityService.DataContext.Courses.FirstOrDefault(course => group.CourseId==course.Id);
            group.Department=DataEntityService.DataContext.Departments.FirstOrDefault(department => group.DepartmentId==department.Id);
            group.Users=DataEntityService.DataContext.Users.Where(user=>user.GroupId==group.Id).ToList();
            group.StaticSchedules=DataEntityService.DataContext.Subjects.Where(subject=>subject.GroupId==group.Id).ToList();
        }
        return groups;
    }

    /// <summary>
    ///     Сохранить группы.
    /// </summary>
    /// <param name="entities">Список групп.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Group> entities)
    {
        var status = DataEntityService.Set(DataEntityService.DataContext.Groups, entities);

        if (!status)
        {
            return BadRequest("No groups were saved!");
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
        var status = DataEntityService.Remove(DataEntityService.DataContext.Groups, ids);

        if (!status)
        {
            return BadRequest("No groups were deleted!");
        }

        return Ok();
    }
}