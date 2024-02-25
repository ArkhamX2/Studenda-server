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
    ///     Получить список прогулов по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="dates">Даты.</param>
    /// <returns>Список прогулов.</returns>
    /// <exception cref="ArgumentException">При некорректных аргументах.</exception>
    public async Task<List<Absence>> GetByUser(int userId, List<DateTime> dates)
    {
        if (userId <= 0)
        {
            throw new ArgumentException("Invalid user id!");
        }

        if (dates.Count <= 0)
        {
            throw new ArgumentException("Invalid dates!");
        }

        var datesHashSet = new HashSet<DateTime>(dates.Select(date => new DateTime(date.Year, date.Month, date.Day)));

        return await DataContext.Absences
            .Where(absence => absence.UserId == userId
                && absence.CreatedAt.HasValue
                && datesHashSet.Contains(absence.CreatedAt.Value.Date))
            .ToListAsync();
    }

    /// <summary>
    ///     Получить список прогулов по датам.
    /// </summary>
    /// <param name="userIds">Идентификаторы пользователей.</param>
    /// <param name="date">Дата.</param>
    /// <returns>Список прогулов.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов пользователей.</exception>
    public async Task<List<Absence>> GetByDate(List<int> userIds, DateTime date)
    {
        if (userIds.Count <= 0)
        {
            throw new ArgumentException("Invalid user ids!");
        }

        return await DataContext.Absences
            .Where(absence => userIds.Contains(absence.UserId)
                && absence.CreatedAt.HasValue
                && absence.CreatedAt.Value.Date == date.Date)
            .ToListAsync();
    }
}