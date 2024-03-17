using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа старосты.
/// </summary>
public class LeaderAuthorizationRequirement : IPermissionAuthorizationRequirement
{
    public const string PolicyCode = "LeaderAuthorizationPolicy";

    /// <summary>
    ///     Получить требуемые доступы.
    /// </summary>
    /// <returns>Список доступов.</returns>
    public IEnumerable<string> GetRequiredPermissions()
    {
        return [
            PermissionConfiguration.LeaderPermission,
            PermissionConfiguration.TeacherPermission,
            PermissionConfiguration.AdminPermission
        ];
    }
}