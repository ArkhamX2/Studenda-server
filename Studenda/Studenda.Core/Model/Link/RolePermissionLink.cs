using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Model.Account;

namespace Studenda.Core.Model.Link;

/// <summary>
/// Связь многие ко многим для <see cref="Account.Role"/> и <see cref="Account.Permission"/>.
/// </summary>
public class RolePermissionLink
{
    /// <summary>
    /// Конфигурация модели <see cref="RolePermissionLink"/>.
    /// </summary>
    internal class Configuration : IEntityTypeConfiguration<RolePermissionLink>
    {
        /// <summary>
        /// Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public void Configure(EntityTypeBuilder<RolePermissionLink> builder)
        {
            builder.HasKey(link => new
            {
                link.RoleId,
                link.PermissionId
            });

            builder.HasOne(link => link.Role)
                .WithMany(role => role.RolePermissionLinks)
                .HasForeignKey(link => link.RoleId)
                .IsRequired(IsRoleIdRequired);

            builder.HasOne(link => link.Permission)
                .WithMany(permission => permission.RolePermissionLinks)
                .HasForeignKey(link => link.PermissionId)
                .IsRequired(IsPermissionIdRequired);
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
    /// Статус необходимости наличия значения в поле <see cref="RoleId"/>.
    /// </summary>
    public const bool IsRoleIdRequired = true;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="PermissionId"/>.
    /// </summary>
    public const bool IsPermissionIdRequired = true;

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
    /// Идентификатор связанного объекта <see cref="Account.Role"/>.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Идентификатор связанного объекта <see cref="Account.Permission"/>.
    /// </summary>
    public int PermissionId { get; set; }

    #endregion

    /// <summary>
    /// Связанный объект <see cref="Account.Role"/>.
    /// </summary>
    public Role Role { get; set; } = null!;

    /// <summary>
    /// Связанный объект <see cref="Account.Permission"/>.
    /// </summary>
    public Permission Permission { get; set; } = null!;
}