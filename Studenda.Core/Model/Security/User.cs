using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Security.Management;

namespace Studenda.Core.Model.Security;

/// <summary>
///     Пользователь.
/// </summary>
public class User : Identity
{
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
    ///     Максимальная длина поля <see cref="Name" />.
    /// </summary>
    public const int NameLengthMax = 32;

    /// <summary>
    ///     Максимальная длина поля <see cref="Surname" />.
    /// </summary>
    public const int SurnameLengthMax = 32;

    /// <summary>
    ///     Максимальная длина поля <see cref="Patronymic" />.
    /// </summary>
    public const int PatronymicLengthMax = 32;

    /// <summary>
    ///     Максимальная длина поля <see cref="Login" />.
    /// </summary>
    public const int LoginLengthMax = 128;

    /// <summary>
    ///     Максимальная длина поля <see cref="PasswordHash" />.
    ///     TODO: Необходимо учитывать метод шифрования.
    /// </summary>
    public const int PasswordHashLengthMax = 256;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="RoleId" />.
    /// </summary>
    public const bool IsRoleIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="GroupId" />.
    /// </summary>
    public const bool IsGroupIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="DepartmentId" />.
    /// </summary>
    public const bool IsDepartmentIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Name" />.
    /// </summary>
    public const bool IsNameRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Surname" />.
    /// </summary>
    public const bool IsSurnameRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Patronymic" />.
    /// </summary>
    public const bool IsPatronymicRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Login" />.
    /// </summary>
    public const bool IsLoginRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="PasswordHash" />.
    /// </summary>
    public const bool IsPasswordHashRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="User" />.
    /// </summary>
    internal class Configuration : Configuration<User>
    {
        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        public Configuration(ContextConfiguration configuration) : base(configuration)
        {
            // PASS.
        }

        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(user => user.Surname)
                .HasMaxLength(SurnameLengthMax)
                .IsRequired(IsSurnameRequired);

            builder.Property(user => user.Patronymic)
                .HasMaxLength(PatronymicLengthMax)
                .IsRequired(IsPatronymicRequired);

            builder.Property(user => user.Login)
                .HasMaxLength(LoginLengthMax)
                .IsRequired();

            builder.Property(user => user.PasswordHash)
                .HasMaxLength(PasswordHashLengthMax)
                .IsRequired();

            builder.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .IsRequired();
            
            builder.HasOne(user => user.Group)
                .WithMany(group => group.Users)
                .HasForeignKey(user => user.GroupId)
                .IsRequired(IsGroupIdRequired);
            
            builder.HasOne(user => user.Department)
                .WithMany(department => department.Users)
                .HasForeignKey(user => user.DepartmentId)
                .IsRequired(IsDepartmentIdRequired);

            base.Configure(builder);
        }
    }

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
    ///     Идентификатор связанного объекта <see cref="Role" />.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Group" />.
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Department" />.
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    ///     Имя.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Фамилия.
    ///     Необязательное поле.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    ///     Отчество.
    ///     Необязательное поле.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Login { get; set; } = null!;

    /// <summary>
    ///     Хеш пароля.
    ///     TODO: Разобраться с методом шифрования.
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Role" />.
    /// </summary>
    public Role Role { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Group" />.
    /// </summary>
    public Group? Group { get; set; }

    /// <summary>
    ///     Связанный объект <see cref="Department" />.
    /// </summary>
    public Department? Department { get; set; }
}