using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Security.Management;

/// <summary>
///     Связь многие ко многим для <see cref="Management.Role" /> и <see cref="Management.Permission" />.
/// </summary>
public class RolePermissionLink : Entity
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
    ///     Статус необходимости наличия значения в поле <see cref="RoleId" />.
    /// </summary>
    public const bool IsRoleIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="PermissionId" />.
    /// </summary>
    public const bool IsPermissionIdRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="RolePermissionLink" />.
    /// </summary>
    internal class Configuration : Configuration<RolePermissionLink>
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
        public override void Configure(EntityTypeBuilder<RolePermissionLink> builder)
        {
            builder.HasKey(link => new
            {
                link.RoleId,
                link.PermissionId
            });

            builder.HasOne(link => link.Role)
                .WithMany(role => role.RolePermissionLinks)
                .HasForeignKey(link => link.RoleId)
                .IsRequired();

            builder.HasOne(link => link.Permission)
                .WithMany(permission => permission.RolePermissionLinks)
                .HasForeignKey(link => link.PermissionId)
                .IsRequired();
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
    ///     Идентификатор связанного объекта <see cref="Management.Role" />.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.Permission" />.
    /// </summary>
    public int PermissionId { get; set; }

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Management.Role" />.
    /// </summary>
    public Role Role { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Management.Permission" />.
    /// </summary>
    public Permission Permission { get; set; } = null!;
}