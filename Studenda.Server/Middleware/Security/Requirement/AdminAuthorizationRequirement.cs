using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа администратора.
/// </summary>
public class AdminAuthorizationRequirement : IPermissionAuthorizationRequirement
{
    public const string PolicyCode = "AdminAuthorizationPolicy";

    /// <summary>
    ///     Получить требуемые доступы.
    /// </summary>
    /// <returns>Список доступов.</returns>
    public IEnumerable<string> GetRequiredPermissions()
    {
        return [
            PermissionConfiguration.AdminPermission
        ];
    }
}