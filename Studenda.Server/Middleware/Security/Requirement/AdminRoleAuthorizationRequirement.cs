using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для роли администратора.
/// </summary>
public class AdminRoleAuthorizationRequirement : IRoleAuthorizationRequirement
{
    public const string AuthorizationPolicyCode = "AdminAuthorizationPolicy";

    /// <summary>
    ///     Получить название требуемой роли.
    /// </summary>
    /// <returns>Название роли.</returns>
    public string GetRequiredIdentityRoleName()
    {
        return IdentityRoleConfiguration.AdminRoleName;
    }
}