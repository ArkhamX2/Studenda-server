using Microsoft.AspNetCore.Identity;
using Studenda.Core.Server.Security.Data;

namespace Studenda.Core.Server.Security.Service.Token;

public interface ITokenService
{
    string CreateToken(Account account, IEnumerable<IdentityRole> roles);
}