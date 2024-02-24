using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Studenda.Core.Server.Security.Service;

public class TokenService
{
    public string CreateToken(IdentityUser identityUser, IEnumerable<IdentityRole> roles)
    {
        var token = identityUser.CreateClaims(roles).CreateJwtToken();
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}