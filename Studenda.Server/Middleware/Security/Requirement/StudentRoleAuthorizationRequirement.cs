using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для роли студента.
/// </summary>
public class StudentRoleAuthorizationRequirement : IRoleAuthorizationRequirement
{
    public const string AuthorizationPolicyCode = "StudentAuthorizationPolicy";

    /// <summary>
    ///     Получить название требуемой роли.
    /// </summary>
    /// <returns>Название роли.</returns>
    public string GetRequiredIdentityRoleName()
    {
        return IdentityRoleConfiguration.StudentRoleName;
    }
}