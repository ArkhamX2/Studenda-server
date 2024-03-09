using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для роли преподавателя.
/// </summary>
public class TeacherRoleAuthorizationRequirement : IRoleAuthorizationRequirement
{
    public const string AuthorizationPolicyCode = "TeacherAuthorizationPolicy";

    /// <summary>
    ///     Получить название требуемой роли.
    /// </summary>
    /// <returns>Название роли.</returns>
    public string GetRequiredIdentityRoleName()
    {
        return IdentityRoleConfiguration.TeacherRoleName;
    }
}