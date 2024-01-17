namespace Studenda.Core.Data.Transfer.Security;

/// <summary>
///     Тело запроса модуля безопасности.
/// </summary>
public class SecurityRequest
{
    /// <summary>
    ///     Почта.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    ///     Пароль.
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    ///     Название роли.
    /// </summary>
    public required string RoleName { get; init; }
}