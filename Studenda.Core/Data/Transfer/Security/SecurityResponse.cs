namespace Studenda.Core.Data.Transfer.Security;

/// <summary>
///     Тело ответа модуля безопасности.
/// </summary>
public class SecurityResponse
{
    /// <summary>
    ///     Почта.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    ///     Токен.
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    ///     Токен обновления.
    /// </summary>
    public string RefreshToken { get; set; } = null!;
}