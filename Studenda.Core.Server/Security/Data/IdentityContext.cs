using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Server.Security.Data;

public class IdentityContext : IdentityDbContext<Account>
{
    public IdentityContext(ContextConfiguration configuration)
    {
        Configuration = configuration;

        // TODO: Использовать асинхронные запросы.
        if (!Database.CanConnect())
        {
            if (!Database.EnsureCreated())
            {
                throw new Exception("Connection error!");
            }
        }
        else
        {
            Database.EnsureCreated();
        }
    }

    private ContextConfiguration Configuration { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Configuration.ConfigureContext(optionsBuilder);

        base.OnConfiguring(optionsBuilder);
    }
}