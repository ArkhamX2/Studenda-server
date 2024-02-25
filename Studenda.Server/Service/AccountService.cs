using Microsoft.EntityFrameworkCore;
using Studenda.Server.Data;
using Studenda.Server.Model.Common;

namespace Studenda.Server.Service;

/// <summary>
///     Сервис для работы с <see cref="Account" />.
/// </summary>
/// <param name="dataContext">Контекст данных.</param>
public class AccountService(DataContext dataContext) : DataEntityService(dataContext)
{
    /// <summary>
    ///     Получить список аккаунтов по идентификаторам групп.
    /// </summary>
    /// <param name="groupIds">Идентификаторы групп.</param>
    /// <returns>Список аккаунтов.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов групп.</exception>
    public async Task<List<Account>> GetByGroup(List<int> groupIds)
    {
        if (groupIds.Count <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        return await DataContext.Accounts
            .Where(account => groupIds.Contains(account.GroupId!.Value))
            .ToListAsync();
    }
}