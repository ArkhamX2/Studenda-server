using Studenda.Server.Model.Security;

namespace Studenda.Server.Data.Transfer.Security;

/// <summary>
///     Тело ответа модуля безопасности.
/// </summary>
public class SecurityResponse
{
    /// <summary>
    ///     Объект <see cref="Model.Security.User" />.
    /// </summary>
    public required User User { get; init; }

    /// <summary>
    ///     Токен.
    /// </summary>
    public required string Token { get; set; }
}