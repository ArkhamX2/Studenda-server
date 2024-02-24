using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data;
using Studenda.Core.Model.Journal;
using Studenda.Core.Server.Common.Service;

namespace Studenda.Core.Server.Journal.Service;

/// <summary>
///     Сервис для работы с <see cref="Mark" />.
/// </summary>
/// <param name="dataContext">Контекст данных.</param>
public class MarkService(DataContext dataContext) : DataEntityService(dataContext)
{
    /// <summary>
    ///     Получить список оценок по идентификаторам заданий.
    /// </summary>
    /// <param name="taskIds">Идентификаторы заданий.</param>
    /// <returns>Список оценок.</returns>
    /// <exception cref="ArgumentException">При пустом списке идентификаторов заданий.</exception>
    public async Task<List<Mark>> GetByTask(List<int> taskIds)
    {
        if (taskIds.Count <= 0)
        {
            throw new ArgumentException("Invalid task ids!");
        }

        return await DataContext.Marks
            .Where(mark => taskIds.Contains(mark.TaskId))
            .ToListAsync();
    }
}