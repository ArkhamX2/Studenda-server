using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Schedule;

namespace Studenda.Server.Model.Journal;

/// <summary>
///     Учебная сессия.
/// </summary>
public class Session : IdentifiableEntity
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

    public const bool IsSubjectIdRequired = true;
    public const bool IsStartedAtRequired = true;

    /// <summary>
    ///     Конфигурация модели.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Session>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasOne(session => session.Subject)
                .WithMany(subject => subject.Sessions)
                .HasForeignKey(session => session.SubjectId)
                .IsRequired();

            builder.Property(session => session.StartedAt)
                .HasColumnType(ContextConfiguration.DateTimeType)
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
    ///     Идентификатор связанного объекта <see cref="Schedule.Subject" />.
    /// </summary>
    public required int SubjectId { get; set; }

    /// <summary>
    ///     Дата начала.
    /// </summary>
    public DateTime? StartedAt { get; set; }

    #endregion

    public Subject? Subject { get; set; }
    public List<Absence> Absences { get; set; } = [];
}