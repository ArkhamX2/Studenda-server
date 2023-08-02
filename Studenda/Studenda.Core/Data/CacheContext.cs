using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Studenda.Model.Data.Configuration;

namespace Studenda.Model.Data;

/// <summary>
/// Сессия работы с внутренним хранилищем устройства.
/// Используется для кеширования дольших объемов
/// редко изменяющихся данных.
/// </summary>
public sealed class CacheContext : DataContext
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public CacheContext(DatabaseConfiguration configuration) : base(configuration)
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// Обработать инициализацию сессии.
    /// Используется для настройки сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=cache.db");

#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
#endif

        base.OnConfiguring(optionsBuilder);
    }
}
