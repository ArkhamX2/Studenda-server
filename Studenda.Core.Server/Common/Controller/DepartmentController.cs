using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Common;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Common.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="Department" />.
/// </summary>
[Route("api/department")]
[ApiController]
public class DepartmentController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    public DepartmentController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; }

    /// <summary>
    ///     Получить список факультетов.
    ///     Если идентификатор не указан, возвращается список со всеми факультетами.
    ///     Иначе возвращается список с одним факультетом, либо пустой список.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Результат операции со списком факультетов.</returns>
    [HttpGet]
    public ActionResult<List<Department>> Get([FromQuery] int id)
    {
        var departments= DataEntityService.Get(DataEntityService.DataContext.Departments, id);
        foreach (var department in departments)
        {
            department.Groups=DataEntityService.DataContext.Groups.Where(Group => Group.DepartmentId == department.Id).ToList();
        }
        return departments;
    }

    /// <summary>
    ///     Сохранить факультеты.
    /// </summary>
    /// <param name="entities">Список факультетов.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] List<Department> entities)
    {
        var status = DataEntityService.Set(DataEntityService.DataContext.Departments, entities);

        if (!status)
        {
            return BadRequest("No departments were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить факультеты.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = DataEntityService.Remove(DataEntityService.DataContext.Departments, ids);

        if (!status)
        {
            return BadRequest("No departments were deleted!");
        }

        return Ok();
    }
}