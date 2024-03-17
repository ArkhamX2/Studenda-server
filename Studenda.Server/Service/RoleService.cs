using Microsoft.EntityFrameworkCore;
using Studenda.Server.Data;
using Studenda.Server.Model.Common;

namespace Studenda.Server.Service;

/// <summary>
///     Сервис для работы с <see cref="Role" />.
/// </summary>
/// <param name="dataContext">Контекст данных.</param>
public class RoleService(DataContext dataContext) : DataEntityService(dataContext)
{
    /// <summary>
    ///     Получить роль по-умолчанию.
    /// </summary>
    /// <returns>Список ролей.</returns>
    /// <exception cref="ArgumentException">При пустом списке доступов.</exception>
    public async Task<Role> GetDefault()
    {
        // TODO: Может отсутствовать роль по-умолчанию?
        return await DataContext.Roles
            .Where(role => role.IsDefault)
            .FirstAsync();
    }

    /// <summary>
    ///     Получить список ролей по идентификаторам аккаунтов.
    /// </summary>
    /// <param name="accountIds">Идентификаторы аккаунтов.</param>
    /// <returns>Список ролей.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов.</exception>
    public async Task<List<Role>> GetByAccount(List<int> accountIds)
    {
        if (accountIds.Count <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        var accounts = await DataContext.Accounts
            .Where(account => accountIds.Contains(account.Id.GetValueOrDefault()))
            .ToListAsync();

        var roleIds = accounts
            .Select(account => account.RoleId)
            .ToList();

        return await DataContext.Roles
            .Where(role => roleIds.Contains(role.Id.GetValueOrDefault()))
            .ToListAsync();
    }

    /// <summary>
    ///     Получить список ролей по доступам.
    /// </summary>
    /// <param name="permissions">Доступы.</param>
    /// <returns>Список ролей.</returns>
    /// <exception cref="ArgumentException">При пустом списке доступов.</exception>
    public async Task<List<Role>> GetByPermission(List<string> permissions)
    {
        if (permissions.Count <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        return await DataContext.Roles
            .Where(role => permissions.Contains(role.Permission))
            .ToListAsync();
    }
}