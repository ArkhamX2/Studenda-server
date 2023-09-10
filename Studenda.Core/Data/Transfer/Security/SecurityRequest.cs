namespace Studenda.Core.Data.Transfer.Security;

/// <summary>
///     Тело запроса модуля безопасности.
/// </summary>
public class SecurityRequest
{
    /// <summary>
    ///     Почта.
    /// </summary>
    public string Email { get; init; } = null!;

    /// <summary>
    ///     Пароль.
    /// </summary>
    public string Password { get; init; } = null!;
}