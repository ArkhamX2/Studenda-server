using Microsoft.EntityFrameworkCore;

namespace Studenda.Core.Data.Configuration;

/// <summary>
///     Конфигурация контекста для работы с базами данных MySQL и MariaDB.
/// </summary>
public class MysqlConfiguration : ContextConfiguration
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <param name="serverVersion">Версия сервера базы данных.</param>
    /// <param name="isDebugMode">Статус конфигурации для разработки.</param>
    public MysqlConfiguration(string connectionString, ServerVersion serverVersion, bool isDebugMode) : base(
        connectionString, isDebugMode)
    {
        ServerVersion = serverVersion;
    }

    /// <summary>
    ///     Версия сервера базы данных.
    /// </summary>
    private ServerVersion ServerVersion { get; }

    /// <summary>
    ///     Тип полей даты и времени в базе данных.
    /// </summary>
    internal override string DateTimeType => "DATETIME";

    /// <summary>
    ///     Указатель использования текущих даты и времени
    ///     для полей типа <see cref="DateTimeType" /> в базе данных.
    /// </summary>
    internal override string DateTimeValueCurrent => "CURRENT_TIMESTAMP";

    /// <summary>
    ///     Применить настройки к сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    public override void ConfigureContext(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(ConnectionString, ServerVersion);

        base.ConfigureContext(optionsBuilder);
    }
}