using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data;
using Studenda.Core.Model.Security;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Journal.Service;

/// <summary>
///     Сервис для работы с <see cref="User" />.
/// </summary>
/// <param name="dataContext">Контекст данных.</param>
public class UserService(DataContext dataContext) : DataEntityService(dataContext)
{
    /// <summary>
    ///     Получить список пользователей по идентификаторам групп.
    /// </summary>
    /// <param name="groupIds">Идентификаторы групп.</param>
    /// <returns>Список пользователей.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов групп.</exception>
    public async Task<List<User>> GetByGroup(List<int> groupIds)
    {
        if (groupIds.Count <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        return await DataContext.Users
            .Where(user => groupIds.Contains(user.GroupId!.Value))
            .ToListAsync();
    }
}