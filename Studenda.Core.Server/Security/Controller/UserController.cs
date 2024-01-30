﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Model.Security;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Security.Controller;

/// <summary>
///     Контроллер для работы с объектами типа <see cref="User" />.
/// </summary>
[Route("api/security/user")]
[ApiController]
public class UserController : ControllerBase
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    public UserController(DataEntityService dataEntityService)
    {
        DataEntityService = dataEntityService;
    }

    /// <summary>
    ///     Сервис моделей.
    /// </summary>
    private DataEntityService DataEntityService { get; }

    /// <summary>
    ///     Получить список всех пользователей.
    /// </summary>
    /// <returns>Результат операции со списком пользователей.</returns>
    [HttpGet("all")]
    public ActionResult<List<User>> GetAll()
    {
        return DataEntityService.Get(DataEntityService.DataContext.Users, new List<int>());
    }

    /// <summary>
    ///     Получить список пользователей.
    ///     Если идентификаторы не указаны, возвращается список со всеми пользователями.
    ///     Иначе возвращается список с указанными пользователями, либо пустой список.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции со списком пользователей.</returns>
    [HttpGet]
    public ActionResult<List<User>> Get([FromQuery] List<int> ids)
    {
        return DataEntityService.Get(DataEntityService.DataContext.Users, ids);
    }

    /// <summary>
    ///     Сохранить пользователей.
    /// </summary>
    /// <param name="entities">Список пользователей.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpPost]
    public IActionResult Post([FromBody] List<User> entities)
    {
        var status = DataEntityService.Set(DataEntityService.DataContext.Users, entities);

        if (!status)
        {
            return BadRequest("No users were saved!");
        }

        return Ok();
    }

    /// <summary>
    ///     Удалить пользователей.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpDelete]
    public IActionResult Delete([FromBody] List<int> ids)
    {
        var status = DataEntityService.Remove(DataEntityService.DataContext.Users, ids);

        if (!status)
        {
            return BadRequest("No users were deleted!");
        }

        return Ok();
    }
}