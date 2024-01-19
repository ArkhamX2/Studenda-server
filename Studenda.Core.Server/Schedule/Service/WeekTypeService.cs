using System.Globalization;
using Studenda.Core.Data;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Schedule.Service;

/// <summary>
///     Сервис для работы с <see cref="WeekType" />.
/// </summary>
public class WeekTypeService : DataEntityService
{
    /// <summary>
    ///    Месяц начала учебного года.
    /// </summary>
    private const int AcademicYearStartMonth = 9;

    /// <summary>
    ///     День начала учебного года.
    /// </summary>
    private const int AcademicYearStartDay = 1;

    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataContext">Контекст данных.</param>
    public WeekTypeService(DataContext dataContext) : base(dataContext)
    {
        // PASS.
    }

    /// <summary>
    ///     Получить текущий тип недели.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <returns>Тип недели или ничего.</returns>
    public WeekType? GetCurrent(int year)
    {
        var startOfAcademicYear = new DateTime(year, AcademicYearStartMonth, AcademicYearStartDay);

        if (DateTime.Now < startOfAcademicYear)
        {
            // Используем прошлый год.
            startOfAcademicYear = startOfAcademicYear.AddYears(-1);
        }

        var calendar = CultureInfo.CurrentCulture.Calendar;
        var currentWeekNumber = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        var startWeekNumber = calendar.GetWeekOfYear(startOfAcademicYear, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        var currentWeek = currentWeekNumber - startWeekNumber + 1;

        var maxIndex = DataContext.WeekTypes.Max(type => type.Index);
        var circularIndex = currentWeek % maxIndex;

        if (circularIndex == 0)
        {
            circularIndex = maxIndex;
        }

        return DataContext.WeekTypes.FirstOrDefault(type => type.Index == circularIndex);
    }

    /// <summary>
    ///     Получить список типов недели, отсортированных по индексу.
    /// </summary>
    /// <returns>Список типов недели.</returns>
    public List<WeekType> GetOrderedByIndex()
    {
        return DataContext.WeekTypes.OrderBy(type => type.Index).ToList();
    }

    /// <summary>
    ///     Задать список типов недели.
    /// </summary>
    /// <param name="weekTypes">Список типов недели.</param>
    /// <returns>Статус операции.</returns>
    public bool Set(List<WeekType> weekTypes)
    {
        if (weekTypes.Count <= 0)
        {
            return false;
        }

        var minIndex = weekTypes.Min(type => type.Index);

        if (DataContext.WeekTypes.Any())
        {
            var maxIndex = DataContext.WeekTypes.Max(type => type.Index);

            if (minIndex - maxIndex > 1)
            {
                return false;
            }
        }
        else if (minIndex != WeekType.StartIndex)
        {
            return false;
        }

        return base.Set(DataContext.WeekTypes, weekTypes);
    }
}