using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для роли старосты.
/// </summary>
public class LeaderRoleAuthorizationRequirement : IRoleAuthorizationRequirement
{
    public const string AuthorizationPolicyCode = "LeaderAuthorizationPolicy";

    /// <summary>
    ///     Получить название требуемой роли.
    /// </summary>
    /// <returns>Название роли.</returns>
    public string GetRequiredIdentityRoleName()
    {
        return IdentityRoleConfiguration.LeaderRoleName;
    }
}