using Microsoft.AspNetCore.Authorization;
using Studenda.Server.Configuration.Repository;
using Studenda.Server.Middleware.Security.Requirement;
using Studenda.Server.Service.Security;
using ConfigurationManager = Studenda.Server.Configuration.ConfigurationManager;

namespace Studenda.Server.Middleware.Security;

/// <summary>
///     Обработчик авторизации для настраиваемых ролей.
/// </summary>
/// <typeparam name="TRequirement">Требование авторизации для ролей.</typeparam>
/// <param name="configurationManager">Менеджер конфигурации.</param>
public class RoleAuthorizationHandler<TRequirement>(
    ConfigurationManager configurationManager)
    : AuthorizationHandler<TRequirement> where TRequirement : IRoleAuthorizationRequirement
{
    private IdentityConfiguration IdentityConfiguration { get; } = configurationManager.IdentityConfiguration;

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
        var roleValue = TokenService.FindTokenClaimValue(userClaims, TokenService.ClaimLabelUserRole);

        if (roleValue is null)
        {
            return Task.CompletedTask;
        }

        var roleNames = TokenService.ConvertEnumerableFromString(roleValue);
        var requiredRoleName = requirement.GetRequiredIdentityRoleName();

        if (roleNames.Contains(requiredRoleName))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}