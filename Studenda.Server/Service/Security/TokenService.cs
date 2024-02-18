using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Configuration;
using Studenda.Server.Model.Security;

namespace Studenda.Server.Service.Security;

public class TokenService(ConfigurationRepository configuration)
{
    private const string ClaimLabelUserId = ClaimTypes.NameIdentifier;
    private const string ClaimLabelUserEmail = ClaimTypes.Email;
    private const string ClaimLabelUserRole = ClaimTypes.Role;

    private ConfigurationRepository Configuration { get; } = configuration;

    public string CreateNewToken(User user, IdentityRole role)
    {
        var claims = CreateTokenClaims(user, role);
        var token = CreateJwtToken(claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static IEnumerable<Claim> CreateTokenClaims(User user, IdentityRole role)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(ClaimLabelUserId, user.Id)
        };

        if (!string.IsNullOrEmpty(user.Email))
        {
            claims.Add(new Claim(ClaimLabelUserEmail, user.Email));
        }

        if (!string.IsNullOrEmpty(role.Name))
        {
            claims.Add(new Claim(ClaimLabelUserRole, role.Name));
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