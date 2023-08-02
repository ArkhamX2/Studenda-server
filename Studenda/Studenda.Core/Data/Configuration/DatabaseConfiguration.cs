namespace Studenda.Model.Data.Configuration;

/// <summary>
/// Конфигурация базы данных.
/// </summary>
public abstract class DatabaseConfiguration
{
    /// <summary>
    /// Тип полей даты и времени.
    /// </summary>
    public abstract string DateTimeType { get; }

    /// <summary>
    /// Указатель использования текущих даты и времени
    /// для полей типа <see cref="DateTimeType"/>.
    /// </summary>
    public abstract string DateTimeValueCurrent { get; }
}
