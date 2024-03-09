using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Configuration.Repository;
using ConfigurationManager = Studenda.Server.Configuration.ConfigurationManager;

namespace Studenda.Server.Service.Security;

/// <summary>
///     Сервис для работы с токенами.
/// </summary>
/// <param name="configuration">Менеджер конфигурации.</param>
public class TokenService(ConfigurationManager configuration)
{
    public const string ClaimLabelUserId = ClaimTypes.NameIdentifier;
    public const string ClaimLabelUserName = ClaimTypes.Name;
    public const string ClaimLabelUserEmail = ClaimTypes.Email;
    public const string ClaimLabelUserRole = ClaimTypes.Role;

    private TokenConfiguration Configuration { get; } = configuration.TokenConfiguration;

    /// <summary>
    ///     Найти в клеймах значение указанного типа.
    /// </summary>
    /// <param name="claims">Набор клеймов.</param>
    /// <param name="claimType">Тип.</param>
    /// <returns>Значение.</returns>
    public static string? FindTokenClaimValue(IEnumerable<Claim> claims, string claimType)
    {
        return claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
    }

    /// <summary>
    ///     Сконвертировать список в стандартное значение клейма.
    /// </summary>
    /// <param name="enumerable">Список.</param>
    /// <returns>Строковое значение.</returns>
    public static string ConvertEnumerableToString(IEnumerable<string?> enumerable)
    {
        return string.Join(", ", enumerable);
    }

    /// <summary>
    ///     Сконвертировать стандартное значение клейма в список.
    /// </summary>
    /// <param name="claimValue">Строковое значение.</param>
    /// <returns>Список.</returns>
    public static IEnumerable<string> ConvertEnumerableFromString(string claimValue)
    {
        return [.. claimValue.Split(", ")];
    }

    /// <summary>
    ///     Создать новый токен.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <param name="roles">Роли.</param>
    /// <returns>Новый токен.</returns>
    public string CreateNewToken(IdentityUser user, IReadOnlyCollection<IdentityRole> roles)
    {
        var claims = CreateTokenClaims(user, roles);
        var lifetime = Configuration.GetLifetimeMinutes();
        var key = Configuration.GetSecurityKey();

        var token = new JwtSecurityToken(
            Configuration.GetIssuer(),
            Configuration.GetAudience(),
            claims,
            expires: DateTime.UtcNow.AddMinutes(lifetime),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    ///     Создать набор клеймов для токена.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <param name="roles">Роли.</param>
    /// <returns>Набор клеймов.</returns>
    private IEnumerable<Claim> CreateTokenClaims(IdentityUser user, IReadOnlyCollection<IdentityRole> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, Configuration.GetClaimNameSub()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new(ClaimLabelUserId, user.Id)
        };

        if (!string.IsNullOrEmpty(user.UserName))
        {
            claims.Add(new Claim(ClaimLabelUserName, user.UserName));
        }

        if (!string.IsNullOrEmpty(user.Email))
        {
            claims.Add(new Claim(ClaimLabelUserEmail, user.Email));
        }

        if (roles.Count > 0)
        {
            claims.Add(new Claim(ClaimLabelUserRole, ConvertEnumerableToString(roles.Select(role => role.Name))));
        }

        return claims;
    }
}