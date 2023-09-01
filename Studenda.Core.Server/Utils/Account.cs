using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Server.Utils;

public class Account : IdentityUser<long>
{
    #region  Entity
    public int AccountId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
    public string? Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    #endregion

    internal class AccountConfiguration: IEntityTypeConfiguration<Account> 
    {
        private readonly ContextConfiguration _contextConfiguration;

        public AccountConfiguration(ContextConfiguration contextConfiguration)
        {
            _contextConfiguration = contextConfiguration;
        }
        

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(user => user.Name).HasMaxLength(MaxNameLenght).IsRequired(IsNameRequired);

            builder.Property(user => user.Surname).HasMaxLength(MaxSurnameLenght).IsRequired(IsSurnameRequired);

            builder.Property(user => user.Patronymic).HasMaxLength(MaxPatronymicLenght).IsRequired(IsPatronymicRequired);

            builder.Property(user => user.Password).HasMaxLength(MaxPasswordLenght).IsRequired(IsPasswordRequired);

            builder.Property(entity => entity.CreatedAt).HasColumnType(_contextConfiguration.DateTimeType).HasDefaultValueSql(_contextConfiguration.DateTimeValueCurrent);

            builder.Property(user => user.UpdateAt).HasColumnType(_contextConfiguration.DateTimeType);
        }
    }
    #region Configuration
    public const int MaxNameLenght = 32;

    public const int MaxPasswordLenght = 32;

    public const int MaxSurnameLenght = 32;

    public const int MaxPatronymicLenght = 32;

    public const bool IsNameRequired = true;

    public const bool IsSurnameRequired = false;

    public const bool IsPatronymicRequired = false;

    public const bool IsPasswordRequired = true;

    private const bool IsCreatedAtRequired = false;

    private const bool IsUpdatedAtRequired = false;

    #endregion
}
