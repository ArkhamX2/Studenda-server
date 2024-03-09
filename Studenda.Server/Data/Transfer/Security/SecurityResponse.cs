using Studenda.Server.Model.Common;

namespace Studenda.Server.Data.Transfer.Security;

/// <summary>
///     Тело ответа модуля безопасности.
/// </summary>
public class SecurityResponse
{
    /// <summary>
    ///     Объект <see cref="Common.Account" />.
    /// </summary>
    public required Account Account { get; init; }

    /// <summary>
    ///     Токен.
    /// </summary>
    public required string Token { get; init; }
}