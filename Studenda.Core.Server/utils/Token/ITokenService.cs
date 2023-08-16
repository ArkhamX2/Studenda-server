using Microsoft.AspNetCore.Identity;

namespace Studenda.Core.Server.utils.Token
{
    public interface ITokenService
    {
        string CreateToken(Person user, List<IdentityRole<long>> role);
    }
}
