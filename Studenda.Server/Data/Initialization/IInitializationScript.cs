namespace Studenda.Server.Data.Initialization;

/// <summary>
///     Скрипт инициализации контекста данных.
/// </summary>
interface IInitializationScript
{
    /// <summary>
    ///     Запустить инициализацию контекста данных.
    /// </summary>
    /// <returns>Операция.</returns>
    public Task Run();
}