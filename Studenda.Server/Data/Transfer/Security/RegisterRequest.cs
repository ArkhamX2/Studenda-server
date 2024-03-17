using Studenda.Server.Model.Security;

namespace Studenda.Server.Data.Transfer.Security;

/// <summary>
///     Тело запроса регистрации.
/// </summary>
public class RegisterRequest : SecurityRequest
{
    /// <summary>
    ///     Аккаунт.
    /// </summary>
    public required Account Account { get; init; }
}