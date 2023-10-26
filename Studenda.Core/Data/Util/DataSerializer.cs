using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Studenda.Core.Data.Util;

/// <summary>
///     Класс-обертка для сериализации данных в формат JSON.
/// </summary>
public static class DataSerializer
{
    /// <summary>
    ///     Сериализовать объект в строку JSON.
    /// </summary>
    /// <param name="rawData">Объект.</param>
    /// <returns>Строка JSON.</returns>
    public static string Serialize(object? rawData)
    {
        return JsonConvert.SerializeObject(rawData, GetConfiguration());
    }

    /// <summary>
    ///     Десериализовать строку JSON в объект указанного типа данных.
    /// </summary>
    /// <param name="serializedData">Строка JSON.</param>
    /// <typeparam name="T">Выходной тип данных.</typeparam>
    /// <returns>Объект указанного выходного типа данных.</returns>
    public static T? Deserialize<T>([StringSyntax("Json")] string serializedData)
    {
        return JsonConvert.DeserializeObject<T>(serializedData, GetConfiguration());
    }

    /// <summary>
    ///     Конфигурация.
    /// </summary>
    private static JsonSerializerSettings? Configuration { get; set; }

    /// <summary>
    ///     Получить конфигурацию.
    /// </summary>
    /// <returns>Конфигурация.</returns>
    private static JsonSerializerSettings GetConfiguration()
    {
        return Configuration ??= new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
    }
}