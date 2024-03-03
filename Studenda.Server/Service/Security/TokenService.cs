using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Configuration;

namespace Studenda.Server.Service.Security;

public class TokenService(ConfigurationRepository configuration)
{
    private const string ClaimLabelUserId = ClaimTypes.NameIdentifier;
    private const string ClaimLabelUserName = ClaimTypes.Name;
    private const string ClaimLabelUserEmail = ClaimTypes.Email;
    private const string ClaimLabelUserRole = ClaimTypes.Role;

    private ConfigurationRepository Configuration { get; } = configuration;

    public string CreateNewToken(IdentityUser user, IReadOnlyCollection<IdentityRole> roles)
    {
        var claims = CreateTokenClaims(user, roles);
        var lifetime = Configuration.GetTokenLifetimeMinutes();
        var key = Configuration.GetTokenSecurityKey();

        var token = new JwtSecurityToken(
            Configuration.GetTokenIssuer(),
            Configuration.GetTokenAudience(),
            claims,
            expires: DateTime.UtcNow.AddMinutes(lifetime),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private IEnumerable<Claim> CreateTokenClaims(IdentityUser user, IReadOnlyCollection<IdentityRole> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, Configuration.GetTokenClaimNameSub()),
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
            claims.Add(new Claim(ClaimLabelUserRole, string.Join(",", roles.Select(role => role.Name))));
        }

        return claims;
    }
}