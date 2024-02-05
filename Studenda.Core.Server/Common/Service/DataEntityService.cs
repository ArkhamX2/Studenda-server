using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data;
using Studenda.Core.Model;

namespace Studenda.Core.Server.Common.Service;

/// <summary>
///     Сервис для работы с моделями.
/// </summary>
public class DataEntityService
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="dataContext">Контекст данных.</param>
    public DataEntityService(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    /// <summary>
    ///    Контекст данных.
    /// </summary>
    internal DataContext DataContext { get; }

    /// <summary>
    ///     Получить модели по списку идентификаторов.
    /// </summary>
    /// <param name="dbSet">Набор объектов <see cref="DbSet{TEntity}" />.</param>
    /// <param name="ids">Список идентификаторов.</param>
    /// <typeparam name="TSource">Тип модели.</typeparam>
    /// <returns>Список моделей.</returns>
    public async Task<List<TSource>> Get<TSource>(DbSet<TSource> dbSet, List<int> ids) where TSource : Identity
    {
        if (ids.Count <= 0)
        {
            return await dbSet.ToListAsync();
        }

        return await dbSet.Where(identity => ids.Contains(identity.Id.GetValueOrDefault())).ToListAsync();
    }

    /// <summary>
    ///     Сохранить модели.
    /// </summary>
    /// <param name="dbSet">Набор объектов <see cref="DbSet{TEntity}" />.</param>
    /// <param name="entities">Список моделей.</param>
    /// <typeparam name="TSource">Тип модели.</typeparam>
    /// <returns>Статус операции.</returns>
    public async Task<bool> Set<TSource>(DbSet<TSource> dbSet, List<TSource> entities) where TSource : Identity
    {
        if (entities.Count <= 0)
        {
            return false;
        }

        var newEntities = entities.Where(entity => !entity.Id.HasValue).ToList();
        var oldEntities = entities.Where(entity => entity.Id.HasValue).ToList();

        var oldIds = oldEntities.Select(entity => entity.Id.GetValueOrDefault()).ToList();
        var oldIdsInDb = dbSet
            .Where(entity => oldIds.Contains(entity.Id.GetValueOrDefault()))
            .Select(entity => entity.Id.GetValueOrDefault()).ToList();
        var oldIdsNotInDb = oldIds.Except(oldIdsInDb).ToList();

        newEntities.AddRange(oldEntities.Where(entity => oldIdsNotInDb.Contains(entity.Id.GetValueOrDefault())));
        oldEntities.RemoveAll(entity => oldIdsNotInDb.Contains(entity.Id.GetValueOrDefault()));

        if (newEntities.Any())
        {
            await dbSet.AddRangeAsync(newEntities);
        }

        if (oldEntities.Any())
        {
            dbSet.UpdateRange(oldEntities);
        }

        return await DataContext.SaveChangesAsync() > 0;
    }

    /// <summary>
    ///     Удалить модели.
    /// </summary>
    /// <param name="dbSet">Набор объектов <see cref="DbSet{TEntity}" />.</param>
    /// <param name="ids">Список идентификаторов.</param>
    /// <typeparam name="TSource">Тип модели.</typeparam>
    /// <returns>Статус операции.</returns>
    public async Task<bool> Remove<TSource>(DbSet<TSource> dbSet, List<int> ids) where TSource : Identity
    {
        if (ids.Count <= 0)
        {
            return false;
        }

        dbSet.RemoveRange(dbSet.Where(
            identity => ids.Contains(identity.Id.GetValueOrDefault())));

        return await DataContext.SaveChangesAsync() > 0;
    }
}