using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Server.Utils;


namespace Studenda.Core.Server.Data
{
    public sealed class ServerDataContext : IdentityDbContext<Account, IdentityRole<long>, long>
    {
        public ServerDataContext(ContextConfiguration configuration)
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

        public DbSet<Account> Accounts => Set<Account>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Configuration.ConfigureContext(optionsBuilder);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Account.AccountConfiguration(Configuration));        

            base.OnModelCreating(modelBuilder);
        }

    }
}
