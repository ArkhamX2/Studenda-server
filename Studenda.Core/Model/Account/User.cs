using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Link;

namespace Studenda.Core.Model.Account;

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
        public Configuration(ContextConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired(IsNameRequired);

            builder.Property(user => user.Surname)
                .HasMaxLength(SurnameLengthMax)
                .IsRequired(IsSurnameRequired);

            builder.Property(user => user.Patronymic)
                .HasMaxLength(PatronymicLengthMax)
                .IsRequired(IsPatronymicRequired);

            builder.Property(user => user.Email)
                .HasMaxLength(EmailLengthMax)
                .IsRequired(IsEmailRequired);

            builder.Property(user => user.PasswordHash)
                .HasMaxLength(PasswordHashLengthMax)
                .IsRequired(IsPasswordHashRequired);

            builder.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .IsRequired(IsRoleIdRequired);

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
     * Константы, задающие базовые конфигурации полей
     * и ограничения модели.
     */
    #region Configuration

    /// <summary>
    /// Максимальная длина поля <see cref="Name"/>.
    /// </summary>
    public const int NameLengthMax = 32;

    /// <summary>
    /// Максимальная длина поля <see cref="Surname"/>.
    /// </summary>
    public const int SurnameLengthMax = 32;

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
    /// TODO: Необходимо учитывать метод шифрования.
    /// </summary>
    public const int PasswordHashLengthMax = 256;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="RoleId"/>.
    /// </summary>
    public const bool IsRoleIdRequired = true;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="Name"/>.
    /// </summary>
    public const bool IsNameRequired = true;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="Surname"/>.
    /// </summary>
    public const bool IsSurnameRequired = false;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="Patronymic"/>.
    /// </summary>
    public const bool IsPatronymicRequired = false;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="Email"/>.
    /// </summary>
    public const bool IsEmailRequired = true;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="PasswordHash"/>.
    /// </summary>
    public const bool IsPasswordHashRequired = true;

    #endregion

    /*             _   _ _
     *   ___ _ __ | |_(_) |_ _   _
     *  / _ \ '_ \| __| | __| | | |
     * |  __/ | | | |_| | |_| |_| |
     *  \___|_| |_|\__|_|\__|\__, |
     *                       |___/
     * Поля данных, соответствующие таковым в таблице
     * модели в базе данных.
     */
    #region Entity

    /// <summary>
    /// Идентификатор связанного объекта <see cref="Role"/>.
    /// </summary>
    public int RoleId { get; set; }

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
    /// Связанный объект <see cref="Role"/>.
    /// </summary>
    public Role Role { get; set; } = null!;

    /// <summary>
    /// Связанные объекты <see cref="UserGroupLink"/>.
    /// </summary>
    public List<UserGroupLink> UserGroupLinks { get; set; } = null!;
}
