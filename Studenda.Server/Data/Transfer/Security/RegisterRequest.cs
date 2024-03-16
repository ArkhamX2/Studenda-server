using Studenda.Server.Model.Common;

namespace Studenda.Server.Data.Transfer.Security;

/// <summary>
///     Тело запроса регистрации.
/// </summary>
public class RegisterRequest : SecurityRequest
{
    /// <summary>
    ///     Названия ролей.
    /// </summary>
    public required List<string> RoleNames { get; init; }

    /// <summary>
    ///     Аккаунт.
    /// </summary>
    public Account? Account { get; init; } = null;
}