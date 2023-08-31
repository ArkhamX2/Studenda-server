using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Studenda.Core.Server.Security.Data;

namespace Studenda.Core.Server.Security.Service.Token;

public class TokenService : ITokenService
{
    public TokenService(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public string CreateToken(Account account, IEnumerable<IdentityRole> roles)
    {
        var token = account.CreateClaims(roles).CreateJwtToken(Configuration);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}