using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Schedule;

/// <summary>
///     Замена статичного расписания.
///     Позволяет подменить занятие у конкретного расписания.
/// </summary>
public class ScheduleChange : Identity
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
    ///     Максимальная длина поля <see cref="Description" />.
    /// </summary>
    public const int DescriptionLengthMax = 256;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="StaticScheduleId" />.
    /// </summary>
    public const bool IsStaticScheduleIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="SubjectId" />.
    /// </summary>
    public const bool IsSubjectIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Description" />.
    /// </summary>
    public const bool IsDescriptionRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="ScheduleChange" />.
    /// </summary>
    internal class Configuration : Configuration<ScheduleChange>
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
        public override void Configure(EntityTypeBuilder<ScheduleChange> builder)
        {
            builder.HasOne(change => change.StaticSchedule)
                .WithMany(schedule => schedule.ScheduleChanges)
                .HasForeignKey(change => change.StaticScheduleId)
                .IsRequired();

            builder.HasOne(change => change.Subject)
                .WithMany(subject => subject.ScheduleChanges)
                .HasForeignKey(change => change.SubjectId)
                .IsRequired(IsSubjectIdRequired);

            builder.Property(type => type.Description)
                .HasMaxLength(DescriptionLengthMax)
                .IsRequired(IsDescriptionRequired);

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
    ///     Идентификатор связанного объекта <see cref="Schedule.StaticSchedule" />.
    /// </summary>
    public int StaticScheduleId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Schedule.Subject" />.
    ///     Необязательное поле.
    /// </summary>
    public int SubjectId { get; set; }

    /// <summary>
    ///     Описание.
    ///     Необязательное поле.
    /// </summary>
    public string? Description { get; set; }

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Schedule.StaticSchedule" />.
    /// </summary>
    public StaticSchedule StaticSchedule { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Schedule.Subject" />.
    /// </summary>
    public Subject? Subject { get; set; }
}