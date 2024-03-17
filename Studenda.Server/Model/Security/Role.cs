using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;

namespace Studenda.Server.Model.Security;

/// <summary>
///     Роль аккаунта пользователя.
/// </summary>
public class Role : IdentifiableEntity
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

    public const int PermissionLengthMax = 128;
    public const int NameLengthMax = 32;
    public const bool IsPermissionRequired = true;
    public const bool IsNameRequired = true;
    public const bool IsCanRegisterRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="Role" />.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Role>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(role => role.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(role => role.Permission)
                .HasMaxLength(PermissionLengthMax)
                .IsRequired();

            builder.Property(role => role.CanRegister)
                .IsRequired();

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
    ///     Имя.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Идентификатор в системе безопасности.
    /// </summary>
    public required string Permission { get; set; }

    /// <summary>
    ///     Флаг возможности регистрации.
    /// </summary>
    public required bool CanRegister { get; set; } = false;

    #endregion

    public List<Account> Accounts { get; set; } = [];
}