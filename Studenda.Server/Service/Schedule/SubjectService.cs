using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Data;
using Studenda.Server.Model.Schedule;

namespace Studenda.Server.Service.Schedule;

/// <summary>
///     Сервис для работы с <see cref="Subject" />.
/// </summary>
/// <param name="dataContext">Контекст данных.</param>
public class SubjectService(DataContext dataContext) : DataEntityService(dataContext)
{
    /// <summary>
    ///     Получить список статичных занятий по идентификатору группы.
    /// </summary>
    /// <param name="groupId">Идентификатор группы.</param>
    /// <param name="weekTypeId">Идентификатор типа недели.</param>
    /// <param name="year">Учебный год.</param>
    /// <returns>Список статичных занятий.</returns>
    public async Task<List<Subject>> GetSubjectByGroup(int groupId, int weekTypeId, int year)
    {
        if (groupId <= 0 || weekTypeId <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        return await DataContext.Subjects
            .Where(subject => subject.Group.Id == groupId
                              && subject.WeekType.Id == weekTypeId
                              && subject.AcademicYear == year)
            .OrderBy(subject => subject.CreatedAt)
            .ThenBy(subject => subject.DayPosition.Index)
            .ThenBy(subject => subject.SubjectPosition.Index)
            .ToListAsync();
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="weekTypeId">Идентификатор типа недели.</param>
    /// <param name="year">Учебный год.</param>
    /// <returns>Список статичных занятий.</returns>
    public async Task<List<Subject>> GetSubjectByUser(string userId, int weekTypeId, int year)
    {
        if (userId.IsNullOrEmpty() || weekTypeId <= 0)
        {
            throw new ArgumentException("Invalid arguments!");
        }

        return await DataContext.Subjects
            .Where(subject => subject.User.Id == userId
                              && subject.WeekType.Id == weekTypeId
                              && subject.AcademicYear == year)
            .OrderBy(subject => subject.CreatedAt)
            .ThenBy(subject => subject.DayPosition.Index)
            .ThenBy(subject => subject.SubjectPosition.Index)
            .ToListAsync();
    }
}