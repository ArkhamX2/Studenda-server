using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Account;

public class Account : IdentityUser<long>
{
    #region  Entity
    public int AccountId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
    public string? Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    #endregion

    internal class Configuration 
    {
        private readonly ContextConfiguration _contextConfiguration;

        protected Configuration(ContextConfiguration contextConfiguration)
        {
            _contextConfiguration = contextConfiguration;
        }
        public  void Configure(EntityTypeBuilder<Account> builder)
        {
            
        }

    }
}
