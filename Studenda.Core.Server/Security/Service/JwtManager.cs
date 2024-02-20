using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Studenda.Core.Server.Security.Service;

public static class JwtManager
{
    public const string Issuer = "MyAuthServer";
    public const string Audience = "MyAuthClient";
    private const string Key = "v89h3bh89vh9ve8hc89nv98nn899cnccn998ev80vi809jberh89b";
    private const string JwtRegisteredClaimNamesSub = "rbveer3h535nn3n35nyny5umbbt";
    private const int ExpirationMinutes = 60;

    public static IEnumerable<Claim> CreateClaims(this IdentityUser identityUser, IEnumerable<IdentityRole> identityRoles)
    {
        try
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, JwtRegisteredClaimNamesSub),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new(ClaimTypes.NameIdentifier, identityUser.Id)
            };

            if (identityUser.UserName is not null)
            {
                claims.Add(new Claim(ClaimTypes.Name, identityUser.UserName));
            }

            if (identityUser.Email is not null)
            {
                claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));
            }

            identityRoles = identityRoles.ToList();

            if (identityRoles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, string.Join(" ", identityRoles.Select(role => role.Name))));
            }

            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static JwtSecurityToken CreateJwtToken(this IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var key = GetSymmetricSecurityKey();

        return new JwtSecurityToken(
            Issuer,
            Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(ExpirationMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
    }

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}