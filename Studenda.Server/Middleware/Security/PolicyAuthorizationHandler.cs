using Microsoft.AspNetCore.Authorization;
using Studenda.Server.Middleware.Security.Requirement;
using Studenda.Server.Service.Security;

namespace Studenda.Server.Middleware.Security;

/// <summary>
///     Обработчик авторизации для политики безопасности.
/// </summary>
/// <typeparam name="TRequirement">Требование авторизации.</typeparam>
public class PolicyAuthorizationHandler<TRequirement> : AuthorizationHandler<TRequirement>
    where TRequirement : IPermissionAuthorizationRequirement
{
    /// <summary>
    ///     Обработать авторизацию пользователя.
    /// </summary>
    /// <param name="context">Контекст авторизации.</param>
    /// <param name="requirement">Требование авторизации для ролей.</param>
    /// <returns>Статус операции.</returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
    {
        if (context.User.Identity is null || !context.User.Identity.IsAuthenticated)
        {
            return Task.CompletedTask;
        }

        var userClaims = context.User.Claims;
        var permission = TokenService.FindTokenClaimValue(userClaims, TokenService.ClaimLabelUserPermission);

        if (string.IsNullOrEmpty(permission))
        {
            return Task.CompletedTask;
        }

        var requiredPermission = requirement.GetRequiredPermission();

        if (permission == requiredPermission)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}