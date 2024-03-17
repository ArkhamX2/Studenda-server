using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа по-умолчанию.
/// </summary>
public class DefaultAuthorizationRequirement : IPermissionAuthorizationRequirement
{
    public const string PolicyCode = "DefaultAuthorizationPolicy";

    /// <summary>
    ///     Получить требуемый доступ.
    /// </summary>
    /// <returns>Доступ.</returns>
    public string GetRequiredPermission()
    {
        return PermissionConfiguration.DefaultPermission;
    }
}