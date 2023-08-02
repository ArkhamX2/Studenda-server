using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Model.Data.Configuration;
using Studenda.Model.Shared.Link;

namespace Studenda.Model.Shared.Account;

/// <summary>
/// Пользователь.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Конфигурация модели <see cref="User"/>.
    /// </summary>
    internal class Configuration : Configuration<User>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        public Configuration(DatabaseConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(user => user.Surname)
                .HasMaxLength(SurnameLengthMax);

            builder.Property(user => user.Patronymic)
                .HasMaxLength(PatronymicLengthMax);

            builder.Property(user => user.Email)
                .HasMaxLength(EmailLengthMax)
                .IsRequired();

            builder.Property(user => user.PasswordHash)
                .HasMaxLength(PasswordHashLengthMax)
                .IsRequired();

            builder.HasOne(user => user.UserRole)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.UserRoleId)
                .IsRequired();

            builder.HasMany(user => user.UserGroupLinks)
                .WithOne(link => link.User)
                .HasForeignKey(link => link.UserId);

            base.Configure(builder);
        }
    }
    
    /*                   __ _                       _   _
     *   ___ ___  _ __  / _(_) __ _ _   _ _ __ __ _| |_(_) ___  _ __
     *  / __/ _ \| '_ \| |_| |/ _` | | | | '__/ _` | __| |/ _ \| '_ \
     * | (_| (_) | | | |  _| | (_| | |_| | | | (_| | |_| | (_) | | | |
     *  \___\___/|_| |_|_| |_|\__, |\__,_|_|  \__,_|\__|_|\___/|_| |_|
     *                        |___/
     *
     * Константы, задающие базовые конфигурации полей
     * и ограничения модели.
     */
    #region Configuration

    /// <summary>
    /// Минимальная длина поля <see cref="Name"/>.
    /// </summary>
    public const int NameLengthMin = 1;

    /// <summary>
    /// Максимальная длина поля <see cref="Name"/>.
    /// </summary>
    public const int NameLengthMax = 32;

    /// <summary>
    /// Минимальная длина поля <see cref="Surname"/>.
    /// </summary>
    public const int SurnameLengthMin = 0;

    /// <summary>
    /// Максимальная длина поля <see cref="Surname"/>.
    /// </summary>
    public const int SurnameLengthMax = 32;

    /// <summary>
    /// Минимальная длина поля <see cref="Patronymic"/>.
    /// </summary>
    public const int PatronymicLengthMin = 0;

    /// <summary>
    /// Максимальная длина поля <see cref="Patronymic"/>.
    /// </summary>
    public const int PatronymicLengthMax = 32;

    /// <summary>
    /// Максимальная длина поля <see cref="Email"/>.
    /// </summary>
    public const int EmailLengthMax = 128;

    /// <summary>
    /// Максимальная длина поля <see cref="PasswordHash"/>.
    /// Необходимо учитывать метод шифрования.
    /// </summary>
    public const int PasswordHashLengthMax = 256;

    #endregion

    /*             _   _ _
     *   ___ _ __ | |_(_) |_ _   _
     *  / _ \ '_ \| __| | __| | | |
     * |  __/ | | | |_| | |_| |_| |
     *  \___|_| |_|\__|_|\__|\__, |
     *                       |___/
     *
     * Поля данных, соответствующие таковым в таблице
     * модели в базе данных.
     */
    #region Entity

    /// <summary>
    /// Идентификатор связанного объекта <see cref="Account.UserRole"/>.
    /// </summary>
    public int UserRoleId { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Фамилия.
    /// Необязательное поле.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// Отчество.
    /// Необязательное поле.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Хеш пароля.
    /// TODO: Разобраться с методом шифрования.
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    #endregion

    /// <summary>
    /// Связанный объект <see cref="Account.UserRole"/>.
    /// </summary>
    public UserRole UserRole { get; set; } = null!;

    /// <summary>
    /// Связанный объект <see cref="UserGroupLink"/>.
    /// </summary>
    public List<UserGroupLink> UserGroupLinks { get; set; } = null!;
}
