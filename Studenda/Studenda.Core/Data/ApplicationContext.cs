using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Studenda.Model.Data.Configuration;

namespace Studenda.Model.Data;

/// <summary>
/// Сессия работы с внешней базой данных.
/// Характеризуется большим временем доступа к данным.
///
/// TODO: Добавить поддержку внешней базы данных.
/// </summary>
public sealed class ApplicationContext : DataContext
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    /// <exception cref="Exception"></exception>
    public ApplicationContext(DatabaseConfiguration configuration) : base(configuration)
    {
#if !DEBUG
        if (!Database.CanConnect())
        {
            throw new Exception("Connection error!");
        }
#endif

        Database.EnsureCreated();
    }

    /// <summary>
    /// Обработать инициализацию сессии.
    /// Используется для настройки сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.UseSqlite("Data Source=storage.db");

        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
#else
        throw new NotImplementedException("RELEASE data source is not implemented yet!");
#endif

        base.OnConfiguring(optionsBuilder);
    }
}
