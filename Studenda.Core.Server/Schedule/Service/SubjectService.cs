using Studenda.Core.Data;
using Studenda.Core.Model.Schedule;
using Studenda.Core.Model.Schedule.Management;
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
    public void GetFullSubject(List<Subject> subjects)
    {
        foreach(Subject subject in subjects)
        {
            subject.Discipline=DataContext.Disciplines.FirstOrDefault(discipline => discipline.Id==subject.DisciplineId);
            subject.SubjectPosition=DataContext.SubjectPositions.FirstOrDefault(subjectPosition => subjectPosition.Id == subject.SubjectPositionId);
            subject.DayPosition=DataContext.DayPositions.FirstOrDefault(dayPosition=>dayPosition.Id==subject.DayPositionId);
            subject.WeekType=DataContext.WeekTypes.FirstOrDefault(weekType=>weekType.Id==subject.WeekTypeId);
            subject.SubjectType=DataContext.SubjectTypes.FirstOrDefault(subjectType=>subjectType.Id== subject.SubjectTypeId);
            subject.User=DataContext.Users.FirstOrDefault(user => user.Id==subject.UserId);
            subject.Group=DataContext.Groups.FirstOrDefault(group => group.Id==subject.GroupId);
            subject.ScheduleChanges=DataContext.SubjectChanges.Where(subjectChange=>subjectChange.StaticScheduleId==subject.Id).ToList();
        }
    }
}