using Microsoft.AspNetCore.Authorization;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа.
/// </summary>
public interface IPermissionAuthorizationRequirement : IAuthorizationRequirement
{
    /// <summary>
    ///     Получить требуемый доступ.
    /// </summary>
    /// <returns>Доступ.</returns>
    public string GetRequiredPermission();
}