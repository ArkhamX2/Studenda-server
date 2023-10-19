using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Server.Security.Data;

public class IdentityContext : IdentityDbContext<Account>
{
    public IdentityContext(ContextConfiguration configuration)
    {
        Configuration = configuration;
    }

    private ContextConfiguration Configuration { get; }

    public bool TryInitialize()
    {
        var canConnect = Database.CanConnect();
        var isCreated = Database.EnsureCreated();

        return canConnect || isCreated;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Configuration.ConfigureContext(optionsBuilder);

        base.OnConfiguring(optionsBuilder);
    }
}