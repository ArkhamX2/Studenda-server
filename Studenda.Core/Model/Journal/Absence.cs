using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Schedule;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Journal;

/// <summary>
///     Прогул.
/// </summary>
public class Absence : IdentifiableEntity
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

    public const bool IsUserIdRequired = true;
    public const bool IsSubjectIdRequired = true;

    /// <summary>
    ///     Конфигурация модели.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Absence>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Absence> builder)
        {
            builder.HasOne(absence => absence.Subject)
                .WithMany(subject => subject.Absences)
                .HasForeignKey(absence => absence.SubjectId)
                .IsRequired();

            builder.HasOne(absence => absence.User)
                .WithMany(user => user.Absences)
                .HasForeignKey(absence => absence.UserId)
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
    ///     Идентификатор связанного объекта <see cref="Security.User" />.
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Schedule.Subject" />.
    /// </summary>
    public required int SubjectId { get; set; }

    #endregion

    public User? User { get; set; }
    public Subject? Subject { get; set; }
}