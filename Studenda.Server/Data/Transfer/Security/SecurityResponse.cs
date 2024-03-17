using Studenda.Server.Model.Security;

namespace Studenda.Server.Data.Transfer.Security;

/// <summary>
///     Тело ответа модуля безопасности.
/// </summary>
public class SecurityResponse
{
    /// <summary>
    ///     Аккаунт.
    /// </summary>
    public required Account Account { get; init; }

    /// <summary>
    ///     Токен.
    /// </summary>
    public required string Token { get; init; }
}