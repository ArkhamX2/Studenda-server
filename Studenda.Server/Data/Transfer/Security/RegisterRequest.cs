using Studenda.Server.Model.Security;

namespace Studenda.Server.Data.Transfer.Security;

/// <summary>
///     Тело запроса регистрации.
/// </summary>
public class RegisterRequest : SecurityRequest
{
    /// <summary>
    ///     Доступ.
    /// </summary>
    public required string Permission { get; init; }

    /// <summary>
    ///     Аккаунт.
    /// </summary>
    public Account? Account { get; init; } = null;
}