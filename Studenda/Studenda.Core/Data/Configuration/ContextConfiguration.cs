using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Studenda.Core.Data.Configuration;

/// <summary>
/// Конфигурация контекста для работы с базой данных.
/// </summary>
public abstract class ContextConfiguration
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <param name="isDebugMode">Статус конфигурации для разработки.</param>
    protected ContextConfiguration(string connectionString, bool isDebugMode)
    {
        ConnectionString = connectionString;
        IsDebugMode = isDebugMode;
    }
    
    /// <summary>
    /// Строка подключения к базе данных.
    /// </summary>
    protected string ConnectionString { get; }
    
    /// <summary>
    /// Статус конфигурации для разработки.
    /// </summary>
    public bool IsDebugMode { get; }
    
    /// <summary>
    /// Тип полей даты и времени в базе данных.
    /// </summary>
    public abstract string DateTimeType { get; }

    /// <summary>
    /// Указатель использования текущих даты и времени
    /// для полей типа <see cref="DateTimeType"/> в базе данных.
    /// </summary>
    public abstract string DateTimeValueCurrent { get; }

    /// <summary>
    /// Применить настройки к сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    public virtual void ConfigureContext(DbContextOptionsBuilder optionsBuilder)
    {
        if (!IsDebugMode)
        {
            return;
        }
        
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.ConfigureWarnings(builder => builder.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
}
