namespace Studenda.Model.Data.Configuration;

/// <summary>
/// Конфигурация базы данных SQLite.
/// </summary>
public class SqliteConfiguration : DatabaseConfiguration
{
    /// <summary>
    /// Тип полей даты и времени.
    /// </summary>
    public override string DateTimeType => "TEXT";

    /// <summary>
    /// Указатель использования текущих даты и времени
    /// для полей типа <see cref="DateTimeType"/>.
    /// </summary>
    public override string DateTimeValueCurrent => "CURRENT_TIMESTAMP";
}
