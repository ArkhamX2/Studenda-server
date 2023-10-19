using Studenda.Core.Model.Security;

namespace Studenda.Core.Data.Transfer.Security;

/// <summary>
///     Тело ответа модуля безопасности.
/// </summary>
public class SecurityResponse
{
    /// <summary>
    ///     Объект <see cref="Model.Security.User" />.
    /// </summary>
    public User User { get; init; } = null!;

    /// <summary>
    ///     Токен.
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    ///     Токен обновления.
    /// </summary>
    public string RefreshToken { get; set; } = null!;
}