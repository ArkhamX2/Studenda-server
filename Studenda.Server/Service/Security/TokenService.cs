using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Configuration;

namespace Studenda.Server.Service.Security;

public class TokenService(ConfigurationRepository configuration)
{
    private ConfigurationRepository Configuration { get; } = configuration;

    public string CreateNewToken(IdentityUser identityUser, IdentityRole role)
    {
        var claims = CreateTokenClaims(identityUser, role);
        var token = CreateJwtToken(claims);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    private static IEnumerable<Claim> CreateTokenClaims(IdentityUser identityUser, IdentityRole role)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(ClaimTypes.NameIdentifier, identityUser.Id)
        };

        if (identityUser.Email is not null)
        {
            claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));
        }

        if (!string.IsNullOrEmpty(role.Name))
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        return claims;
    }

    private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims)
    {
        var lifetime = Configuration.GetTokenLifetimeMinutes();
        var key = Configuration.GetTokenSecurityKey();

        return new JwtSecurityToken(
            Configuration.GetTokenIssuer(),
            Configuration.GetTokenAudience(),
            claims,
            expires: DateTime.UtcNow.AddMinutes(lifetime),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
    }
}