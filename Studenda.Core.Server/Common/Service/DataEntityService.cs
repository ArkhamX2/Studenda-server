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
    public List<TSource> Get<TSource>(DbSet<TSource> dbSet, List<int> ids) where TSource : Identity
    {
        if (ids.Count <= 0)
        {
            return dbSet.ToList();
        }

        return dbSet.Where(identity => ids.Contains(identity.Id.GetValueOrDefault())).ToList();
    }

    /// <summary>
    ///     Сохранить модели.
    /// </summary>
    /// <param name="dbSet">Набор объектов <see cref="DbSet{TEntity}" />.</param>
    /// <param name="entities">Список моделей.</param>
    /// <typeparam name="TSource">Тип модели.</typeparam>
    /// <returns>Статус операции.</returns>
    public bool Set<TSource>(DbSet<TSource> dbSet, List<TSource> entities) where TSource : Identity
    {
        if (entities.Count <= 0)
        {
            return false;
        }

        dbSet.UpdateRange(entities);

        return DataContext.SaveChanges() > 0;
    }

    /// <summary>
    ///     Удалить модели.
    /// </summary>
    /// <param name="dbSet">Набор объектов <see cref="DbSet{TEntity}" />.</param>
    /// <param name="ids">Список идентификаторов.</param>
    /// <typeparam name="TSource">Тип модели.</typeparam>
    /// <returns>Статус операции.</returns>
    public bool Remove<TSource>(DbSet<TSource> dbSet, List<int> ids) where TSource : Identity
    {
        if (ids.Count <= 0)
        {
            return false;
        }

        dbSet.RemoveRange(dbSet.Where(
            identity => ids.Contains(identity.Id.GetValueOrDefault())));

        return DataContext.SaveChanges() > 0;
    }
}