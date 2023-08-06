using Microsoft.EntityFrameworkCore;

namespace Studenda.Core.Data.Configuration;

/// <summary>
/// Конфигурация контекста для работы с базой данных SQLite.
/// </summary>
public class SqliteConfiguration : ContextConfiguration
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <param name="isDebugMode">Статус конфигурации для разработки.</param>
    public SqliteConfiguration(string connectionString, bool isDebugMode) : base(connectionString, isDebugMode)
    {
        // PASS.
    }
    
    /// <summary>
    /// Тип полей даты и времени в базе данных.
    /// </summary>
    public override string DateTimeType => "TEXT";

    /// <summary>
    /// Указатель использования текущих даты и времени
    /// для полей типа <see cref="DateTimeType"/> в базе данных.
    /// </summary>
    public override string DateTimeValueCurrent => "CURRENT_TIMESTAMP";

    /// <summary>
    /// Применить настройки к сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    public override void ConfigureContext(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);

        base.ConfigureContext(optionsBuilder);
    }
}
