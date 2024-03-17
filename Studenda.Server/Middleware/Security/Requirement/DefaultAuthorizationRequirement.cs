using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа по-умолчанию.
/// </summary>
public class DefaultAuthorizationRequirement : IPermissionAuthorizationRequirement
{
    public const string PolicyCode = "DefaultAuthorizationPolicy";

    /// <summary>
    ///     Получить требуемые доступы.
    /// </summary>
    /// <returns>Список доступов.</returns>
    public IEnumerable<string> GetRequiredPermissions()
    {
        return [
            PermissionConfiguration.DefaultPermission,
            PermissionConfiguration.LeaderPermission,
            PermissionConfiguration.TeacherPermission,
            PermissionConfiguration.AdminPermission
        ];
    }
}