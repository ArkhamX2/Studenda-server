using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Studenda.Core.Model.Account;

namespace Studenda.Core.Server.Data
{
    public class ServerDataContext : IdentityDbContext<Account, IdentityRole<long>, long>
    {


    }
}
