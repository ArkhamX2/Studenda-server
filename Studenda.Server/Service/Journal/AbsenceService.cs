using Microsoft.EntityFrameworkCore;
using Studenda.Server.Data;
using Studenda.Server.Model.Journal;

namespace Studenda.Server.Service.Journal;

/// <summary>
///     Сервис для работы с <see cref="Absence" />.
/// </summary>
/// <param name="dataContext">Контекст данных.</param>
public class AbsenceService(DataContext dataContext) : DataEntityService(dataContext)
{
    /// <summary>
    ///     Получить список прогулов по идентификатору аккаунта.
    /// </summary>
    /// <param name="accountId">Идентификатор аккаунта.</param>
    /// <param name="sessionIds">Идентификаторы учебных сессий.</param>
    /// <returns>Список прогулов.</returns>
    /// <exception cref="ArgumentException">При некорректных аргументах.</exception>
    public async Task<List<Absence>> GetByAccount(int accountId, List<int> sessionIds)
    {
        if (accountId <= 0)
        {
            throw new ArgumentException("Invalid account id!");
        }

        if (sessionIds.Count <= 0)
        {
            throw new ArgumentException("Invalid session ids!");
        }

        return await DataContext.Absences
            .Where(absence => absence.AccountId == accountId
                && sessionIds.Contains(absence.SessionId))
            .ToListAsync();
    }

    /// <summary>
    ///     Получить список прогулов по идентификатору учебной сессии.
    /// </summary>
    /// <param name="accountIds">Идентификаторы аккаунтов.</param>
    /// <param name="sessionId">Идентификатор учебной сессии.</param>
    /// <returns>Список прогулов.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов аккаунтов.</exception>
    public async Task<List<Absence>> GetBySession(List<int> accountIds, int sessionId)
    {
        if (accountIds.Count <= 0)
        {
            throw new ArgumentException("Invalid account ids!");
        }
        if (sessionId <= 0)
        {
            throw new ArgumentException("Invalid session id!");
        }

        return await DataContext.Absences
            .Where(absence => accountIds.Contains(absence.AccountId)
                && absence.SessionId == sessionId)
            .ToListAsync();
    }
}