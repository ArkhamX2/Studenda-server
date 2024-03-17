using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа администратора.
/// </summary>
public class AdminAuthorizationRequirement : IPermissionAuthorizationRequirement
{
    public const string PolicyCode = "AdminAuthorizationPolicy";

    /// <summary>
    ///     Получить требуемый доступ.
    /// </summary>
    /// <returns>Доступ.</returns>
    public string GetRequiredPermission()
    {
        return PermissionConfiguration.AdminPermission;
    }
}