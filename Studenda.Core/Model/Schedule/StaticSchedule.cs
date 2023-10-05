using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Schedule;

/// <summary>
///     Статичное расписание.
///     Представляет собой занятие в определенный день
///     для определенной группы.
/// </summary>
public class StaticSchedule : Identity
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
    ///     Статус необходимости наличия значения в поле <see cref="DisciplineId" />.
    /// </summary>
    public const bool IsDisciplineIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="SubjectPositionId" />.
    /// </summary>
    public const bool IsSubjectPositionIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="DayPositionId" />.
    /// </summary>
    public const bool IsDayPositionIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="WeekTypeId" />.
    /// </summary>
    public const bool IsWeekTypeIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="SubjectTypeId" />.
    /// </summary>
    public const bool IsSubjectTypeIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="UserId" />.
    /// </summary>
    public const bool IsUserIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="GroupId" />.
    /// </summary>
    public const bool IsGroupIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Description" />.
    /// </summary>
    public const bool IsDescriptionRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="StaticSchedule" />.
    /// </summary>
    internal class Configuration : Configuration<StaticSchedule>
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
        public override void Configure(EntityTypeBuilder<StaticSchedule> builder)
        {
            builder.HasOne(schedule => schedule.Discipline)
                .WithMany(discipline => discipline.StaticSchedules)
                .HasForeignKey(schedule => schedule.DisciplineId)
                .IsRequired();

            builder.HasOne(schedule => schedule.SubjectPosition)
                .WithMany(position => position.StaticSchedules)
                .HasForeignKey(schedule => schedule.SubjectPositionId)
                .IsRequired();

            builder.HasOne(schedule => schedule.DayPosition)
                .WithMany(position => position.StaticSchedules)
                .HasForeignKey(schedule => schedule.DayPositionId)
                .IsRequired();

            builder.HasOne(schedule => schedule.WeekType)
                .WithMany(type => type.StaticSchedules)
                .HasForeignKey(schedule => schedule.WeekTypeId)
                .IsRequired();

            builder.HasOne(schedule => schedule.SubjectType)
                .WithMany(type => type.StaticSchedules)
                .HasForeignKey(schedule => schedule.SubjectTypeId)
                .IsRequired(IsSubjectTypeIdRequired);

            builder.HasOne(schedule => schedule.User)
                .WithMany(user => user.StaticSchedules)
                .HasForeignKey(schedule => schedule.UserId)
                .IsRequired(IsUserIdRequired);

            builder.HasOne(schedule => schedule.Group)
                .WithMany(group => group.StaticSchedules)
                .HasForeignKey(schedule => schedule.GroupId)
                .IsRequired();

            builder.Property(schedule => schedule.Description)
                .HasMaxLength(DescriptionLengthMax)
                .IsRequired(IsDescriptionRequired);

            builder.HasMany(schedule => schedule.ScheduleChanges)
                .WithOne(change => change.StaticSchedule)
                .HasForeignKey(change => change.StaticScheduleId);

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
    ///     Идентификатор связанного объекта <see cref="Management.Discipline" />.
    /// </summary>
    public int DisciplineId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.SubjectPosition" />.
    /// </summary>
    public int SubjectPositionId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.DayPosition" />.
    /// </summary>
    public int DayPositionId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.WeekType" />.
    /// </summary>
    public int WeekTypeId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.SubjectType" />.
    ///     Необязательное поле.
    /// </summary>
    public int SubjectTypeId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Security.User" />.
    ///     Необязательное поле.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Group" />.
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    ///     Описание.
    ///     Необязательное поле.
    /// </summary>
    public string? Description { get; set; }

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Management.Discipline" />.
    /// </summary>
    public Discipline Discipline { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Management.SubjectPosition" />.
    /// </summary>
    public SubjectPosition SubjectPosition { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Management.DayPosition" />.
    /// </summary>
    public DayPosition DayPosition { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Management.WeekType" />.
    /// </summary>
    public WeekType WeekType { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Management.SubjectType" />.
    /// </summary>
    public SubjectType? SubjectType { get; set; }

    /// <summary>
    ///     Связанный объект <see cref="Security.User" />.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    ///     Связанный объект <see cref="Common.Group" />.
    /// </summary>
    public Group Group { get; set; } = null!;

    /// <summary>
    ///     Связанные объекты <see cref="ScheduleChange" />.
    /// </summary>
    public List<ScheduleChange> ScheduleChanges { get; set; } = null!;
}