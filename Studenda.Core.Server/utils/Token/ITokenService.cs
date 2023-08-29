using Microsoft.AspNetCore.Identity;
using Studenda.Core.Model.Account;

namespace Studenda.Core.Server.Utils.Token
{
    public interface ITokenService
    {
        string CreateToken(Account user, List<IdentityRole<long>> role);
    }
}
