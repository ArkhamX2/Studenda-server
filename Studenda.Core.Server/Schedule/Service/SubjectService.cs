using Studenda.Core.Data;
using Studenda.Core.Model.Schedule;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Service;

/// <summary>
///     Сервис для работы со статичными расписаниями.
/// </summary>
public class SubjectService : DataEntityService
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataContext">Контекст данных.</param>
    public SubjectService(DataContext dataContext) : base(dataContext)
    {
        // PASS.
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору группы.
    /// </summary>
    /// <param name="groupId">Идентификатор группы.</param>
    /// <param name="weekType">Идентификатор типа недели.</param>
    /// <returns>Список статичных занятий.</returns>
    public List<Subject> GetSubjectByGroup(int groupId, int weekType)
    {
        if (groupId <= 0 || weekType <= 0)
        {
            return Get(DataContext.Subjects, 0);
        }

        return DataContext.Subjects
            .Where(subject => subject.Group.Id == groupId && subject.WeekType.Index == weekType)
            .OrderBy(subject => subject.DayPosition.Index)
            .ThenBy(subject => subject.SubjectPosition.Index)
            .ToList();
    }

    /// <summary>
    ///     Получить список статичных занятий по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="weekType">Идентификатор типа недели.</param>
    /// <returns>Список статичных занятий.</returns>
    public List<Subject> GetSubjectByUser(int userId, int weekType)
    {
        if (userId <= 0 || weekType <= 0)
        {
            return Get(DataContext.Subjects, 0);
        }

        return DataContext.Subjects
            .Where(subject => subject.User != null && subject.User.Id == userId && subject.WeekType.Index == weekType)
            .OrderBy(subject => subject.DayPosition.Index)
            .ThenBy(subject => subject.SubjectPosition.Index)
            .ToList();
    }
}