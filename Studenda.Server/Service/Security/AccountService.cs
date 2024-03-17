using Microsoft.EntityFrameworkCore;
using Studenda.Server.Data;
using Studenda.Server.Model.Security;

namespace Studenda.Server.Service.Security;

/// <summary>
///     Сервис для работы с <see cref="Account" />.
/// </summary>
/// <param name="dataContext">Контекст данных.</param>
public class AccountService(DataContext dataContext) : DataEntityService(dataContext)
{
    /// <summary>
    ///     Получить список аккаунтов по идентификаторам ролей.
    /// </summary>
    /// <param name="roleIds">Идентификаторы ролей.</param>
    /// <returns>Список аккаунтов.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов.</exception>
    public async Task<List<Account>> GetByRole(List<int> roleIds)
    {
        if (roleIds.Count <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        return await DataContext.Accounts
            .Where(account => roleIds.Contains(account.RoleId))
            .ToListAsync();
    }

    /// <summary>
    ///     Получить список аккаунтов по идентификаторам групп.
    /// </summary>
    /// <param name="groupIds">Идентификаторы групп.</param>
    /// <returns>Список аккаунтов.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов.</exception>
    public async Task<List<Account>> GetByGroup(List<int> groupIds)
    {
        if (groupIds.Count <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        return await DataContext.Accounts
            .Where(account => groupIds.Contains(account.GroupId.GetValueOrDefault()))
            .ToListAsync();
    }

    /// <summary>
    ///     Получить список аккаунтов по идентификаторам пользователей.
    /// </summary>
    /// <param name="identityIds">Идентификаторы пользователей.</param>
    /// <returns>Список аккаунтов.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов.</exception>
    public async Task<List<Account>> GetByIdentityId(List<string> identityIds)
    {
        if (identityIds.Count <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        return await DataContext.Accounts
            .Where(account => identityIds.Contains(account.IdentityId!))
            .ToListAsync();
    }
}