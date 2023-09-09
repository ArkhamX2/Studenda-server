using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Schedule.Link;

/// <summary>
///     Связь многие ко многим для <see cref="Security.User" /> и <see cref="Schedule.Subject" />.
/// </summary>
public class UserSubjectLink : Entity
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
    ///     Статус необходимости наличия значения в поле <see cref="SubjectId" />.
    /// </summary>
    public const bool IsSubjectIdRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="UserSubjectLink" />.
    /// </summary>
    internal class Configuration : Configuration<UserSubjectLink>
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
        public override void Configure(EntityTypeBuilder<UserSubjectLink> builder)
        {
            builder.HasKey(link => new
            {
                link.UserId,
                link.SubjectId
            });

            builder.HasOne(link => link.User)
                .WithMany(role => role.UserSubjectLinks)
                .HasForeignKey(link => link.UserId)
                .IsRequired();

            builder.HasOne(link => link.Subject)
                .WithMany(permission => permission.UserSubjectLinks)
                .HasForeignKey(link => link.SubjectId)
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
    ///     Идентификатор связанного объекта <see cref="Schedule.Subject" />.
    /// </summary>
    public int SubjectId { get; set; }

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Security.User" />.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Schedule.Subject" />.
    /// </summary>
    public Subject Subject { get; set; } = null!;
}