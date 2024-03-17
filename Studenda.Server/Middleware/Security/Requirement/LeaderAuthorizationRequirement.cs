using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа старосты.
/// </summary>
public class LeaderAuthorizationRequirement : IPermissionAuthorizationRequirement
{
    public const string PolicyCode = "LeaderAuthorizationPolicy";

    /// <summary>
    ///     Получить требуемый доступ.
    /// </summary>
    /// <returns>Доступ.</returns>
    public string GetRequiredPermission()
    {
        return PermissionConfiguration.LeaderPermission;
    }
}