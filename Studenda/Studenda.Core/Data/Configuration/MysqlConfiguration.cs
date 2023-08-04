using Microsoft.EntityFrameworkCore;

namespace Studenda.Core.Data.Configuration;

/// <summary>
/// Конфигурация контекста для работы с базами данных MySQL и MariaDB.
/// </summary>
public class MysqlConfiguration : ContextConfiguration
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <param name="isDebugMode">Статус конфигурации для разработки.</param>
    public MysqlConfiguration(string connectionString, bool isDebugMode) : base(connectionString, isDebugMode)
    {
        // PASS.
    }
    
    /// <summary>
    /// Тип полей даты и времени в базе данных.
    /// </summary>
    public override string DateTimeType => "DATETIME";

    /// <summary>
    /// Указатель использования текущих даты и времени
    /// для полей типа <see cref="DateTimeType"/> в базе данных.
    /// </summary>
    public override string DateTimeValueCurrent => "CURRENT_TIMESTAMP";

    /// <summary>
    /// Применить настройки к сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    /// <exception cref="NotImplementedException">По причине отсутствия поддержки.</exception>
    public override void ConfigureContext(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: Добавить зависимость для поддержки MySQL, либо переделать все под другую базу данных.
        // optionsBuilder.UseMysql(ConnectionString);

        // base.ConfigureContext(optionsBuilder);

        throw new NotImplementedException("MySQL is not supported yet!");
    }
}
