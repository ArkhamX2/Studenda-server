using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Link;

/// <summary>
///     Связь многие ко многим для <see cref="Security.User" /> и <see cref="Common.Group" />.
/// </summary>
public class UserGroupLink : Entity
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
    ///     Статус необходимости наличия значения в поле <see cref="UserId" />.
    /// </summary>
    public const bool IsUserIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="GroupId" />.
    /// </summary>
    public const bool IsGroupIdRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="UserGroupLink" />.
    /// </summary>
    internal class Configuration : Configuration<UserGroupLink>
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
        public override void Configure(EntityTypeBuilder<UserGroupLink> builder)
        {
            builder.HasKey(link => new
            {
                link.UserId,
                link.GroupId
            });

            builder.HasOne(link => link.User)
                .WithMany(user => user.UserGroupLinks)
                .HasForeignKey(link => link.UserId)
                .IsRequired();

            builder.HasOne(link => link.Group)
                .WithMany(group => group.UserGroupLinks)
                .HasForeignKey(link => link.GroupId)
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
    ///     Идентификатор связанного объекта <see cref="Security.User" />.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Group" />.
    /// </summary>
    public int GroupId { get; set; }

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Security.User" />.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Common.Group" />.
    /// </summary>
    public Group Group { get; set; } = null!;
}