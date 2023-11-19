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
    ///     Получить модель по ее идентификатору.
    /// </summary>
    /// <param name="dbSet">Набор объектов <see cref="DbSet{TEntity}" />.</param>
    /// <param name="id">Идентификатор.</param>
    /// <typeparam name="TSource">Тип модели.</typeparam>
    /// <returns>Модель.</returns>
    public static List<TSource> HandleGet<TSource>(DbSet<TSource> dbSet, int id) where TSource : Identity
    {
        if (id <= 0)
        {
            return dbSet.ToList();
        }

        var identity = dbSet.FirstOrDefault(identity => identity.Id == id);
        List<TSource> result = new();

        if (identity != null)
        {
            result.Add(identity);
        }

        return result;
    }

    /// <summary>
    ///     Сохранить модели.
    /// </summary>
    /// <param name="dbSet">Набор объектов <see cref="DbSet{TEntity}" />.</param>
    /// <param name="entities">Список моделей.</param>
    /// <typeparam name="TSource">Тип модели.</typeparam>
    /// <returns>Статус операции.</returns>
    public bool HandlePost<TSource>(DbSet<TSource> dbSet, List<TSource> entities) where TSource : Identity
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
    public bool HandleDelete<TSource>(DbSet<TSource> dbSet, List<int> ids) where TSource : Identity
    {
        if (ids.Count <= 0)
        {
            return false;
        }

        dbSet.RemoveRange(dbSet.Where(identity => ids.Contains(identity.Id)));

        return DataContext.SaveChanges() > 0;
    }
}