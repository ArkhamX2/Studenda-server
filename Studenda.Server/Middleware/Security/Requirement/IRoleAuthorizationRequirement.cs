using Microsoft.AspNetCore.Authorization;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для роли.
/// </summary>
public interface IRoleAuthorizationRequirement : IAuthorizationRequirement
{
    /// <summary>
    ///     Получить название требуемой роли.
    /// </summary>
    /// <returns>Название роли.</returns>
    public string GetRequiredIdentityRoleName();
}