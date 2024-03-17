using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа преподавателя.
/// </summary>
public class TeacherAuthorizationRequirement : IPermissionAuthorizationRequirement
{
    public const string PolicyCode = "TeacherAuthorizationPolicy";

    /// <summary>
    ///     Получить требуемые доступы.
    /// </summary>
    /// <returns>Список доступов.</returns>
    public IEnumerable<string> GetRequiredPermissions()
    {
        return [
            PermissionConfiguration.TeacherPermission,
            PermissionConfiguration.AdminPermission
        ];
    }
}