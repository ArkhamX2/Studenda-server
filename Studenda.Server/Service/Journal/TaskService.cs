using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Data;
using Task = Studenda.Server.Model.Journal.Task;

namespace Studenda.Server.Service.Journal;

/// <summary>
///     Сервис для работы с <see cref="Task" />.
/// </summary>
/// <param name="dataContext">Контекст данных.</param>
public class TaskService(DataContext dataContext) : DataEntityService(dataContext)
{
    /// <summary>
    ///    Месяц начала учебного года.
    ///    TODO: В отдельный класс.
    /// </summary>
    private const int AcademicYearStartMonth = 9;

    /// <summary>
    ///     День начала учебного года.
    ///     TODO: В отдельный класс.
    /// </summary>
    private const int AcademicYearStartDay = 1;

    /// <summary>
    ///     Получить список заданий по идентификатору издателя.
    /// </summary>
    /// <param name="issuerUserId">Идентификатор издателя.</param>
    /// <param name="groupIds">Идентификаторы групп.</param>
    /// <param name="disciplineId">Идентификатор дисциплины или null.</param>
    /// <param name="subjectTypeId">Идентификатор типа занятия или null.</param>
    /// <param name="academicYear">Учебный год или null.</param>
    /// <returns>Список заданий.</returns>
    /// <exception cref="ArgumentException">При некорректных аргументах.</exception>
    public async Task<List<Task>> GetByIssuer(
        int issuerUserId,
        List<int> groupIds,
        int? disciplineId,
        int? subjectTypeId,
        int? academicYear
    ) {
        if (issuerUserId <= 0)
        {
            throw new ArgumentException("Invalid issuer user id!");
        }

        if (groupIds.IsNullOrEmpty())
        {
            throw new ArgumentException("Invalid group ids!");
        }

        var assigneeUserIds = await DataContext.Users
            .Where(user => user.GroupId.HasValue && groupIds.Contains(user.GroupId.Value))
            .Select(user => user.Id)
            .ToListAsync();

        var taskQuery = DataContext.Tasks
            .AsQueryable()
            .Where(task => assigneeUserIds.Contains(task.AssigneeUserId));

        UseDisciplineIdFilter(ref taskQuery, disciplineId);
        UseSubjectTypeIdFilter(ref taskQuery, subjectTypeId);
        UseAcademicYearFilter(ref taskQuery, academicYear);

        return await taskQuery
            .OrderBy(task => task.CreatedAt)
            .ToListAsync();
    }

    /// <summary>
    ///     Получить список заданий по идентификаторам исполнителей.
    /// </summary>
    /// <param name="assigneeUserIds">Идентификаторы исполнителей.</param>
    /// <param name="disciplineId">Идентификатор дисциплины или null.</param>
    /// <param name="subjectTypeId">Идентификатор типа занятия или null.</param>
    /// <param name="academicYear">Учебный год или null.</param>
    /// <returns>Список заданий.</returns>
    /// <exception cref="ArgumentException">При некорректных аргументах.</exception>
    public async Task<List<Task>> GetByAssignee(
        List<int> assigneeUserIds,
        int? disciplineId,
        int? subjectTypeId,
        int? academicYear
    ) {
        if (assigneeUserIds.IsNullOrEmpty())
        {
            throw new ArgumentException("Invalid assignee user ids!");
        }

        var taskQuery = DataContext.Tasks
            .AsQueryable()
            .Where(task => assigneeUserIds.Contains(task.AssigneeUserId));

        UseDisciplineIdFilter(ref taskQuery, disciplineId);
        UseSubjectTypeIdFilter(ref taskQuery, subjectTypeId);
        UseAcademicYearFilter(ref taskQuery, academicYear);

        return await taskQuery
            .OrderBy(task => task.CreatedAt)
            .ToListAsync();
    }

    /// <summary>
    ///     Применить фильтр идентификатора дисциплины к запросу.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="disciplineId">Идентификатор дисциплины или null.</param>
    private static void UseDisciplineIdFilter(ref IQueryable<Task> query, int? disciplineId)
    {
        if (disciplineId is not null)
        {
            query = query.Where(task => task.DisciplineId == disciplineId);
        }
    }

    /// <summary>
    ///     Применить фильтр идентификатора типа занятия к запросу.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="subjectTypeId">Идентификатор типа занятия или null.</param>
    private static void UseSubjectTypeIdFilter(ref IQueryable<Task> query, int? subjectTypeId)
    {
        if (subjectTypeId is not null)
        {
            query = query.Where(task => task.SubjectTypeId == subjectTypeId);
        }
    }

    /// <summary>
    ///     Применить фильтр учебного года к запросу.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="academicYear">Учебный год.</param>
    private static void UseAcademicYearFilter(ref IQueryable<Task> query, int? academicYear)
    {
        if (academicYear is not null)
        {
            var startDate = new DateTime(academicYear.Value, AcademicYearStartMonth, AcademicYearStartDay);
            var endDate = startDate.AddYears(1);

            query = query.Where(task => task.CreatedAt.HasValue
                && task.CreatedAt >= startDate
                && task.CreatedAt < endDate);
        }
    }
}