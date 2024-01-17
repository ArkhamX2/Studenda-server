using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Schedule;

/// <summary>
///     Статичное занятие на определенный день
///     для определенной группы.
/// </summary>
public class Subject : Identity
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
    ///     Максимальная длина поля <see cref="Classroom" />.
    /// </summary>
    public const int ClassroomLengthMax = 32;

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
    ///     Статус необходимости наличия значения в поле <see cref="Classroom" />.
    /// </summary>
    public const bool IsClassroomRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Description" />.
    /// </summary>
    public const bool IsDescriptionRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="Subject" />.
    /// </summary>
    internal class Configuration : Configuration<Subject>
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
        public override void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne(subject => subject.Discipline)
                .WithMany(discipline => discipline.Subjects)
                .HasForeignKey(subject => subject.DisciplineId)
                .IsRequired();

            builder.HasOne(subject => subject.SubjectPosition)
                .WithMany(position => position.StaticSchedules)
                .HasForeignKey(subject => subject.SubjectPositionId)
                .IsRequired();

            builder.HasOne(subject => subject.DayPosition)
                .WithMany(position => position.StaticSchedules)
                .HasForeignKey(subject => subject.DayPositionId)
                .IsRequired();

            builder.HasOne(subject => subject.WeekType)
                .WithMany(type => type.StaticSchedules)
                .HasForeignKey(subject => subject.WeekTypeId)
                .IsRequired();

            builder.HasOne(subject => subject.SubjectType)
                .WithMany(type => type.Subjects)
                .HasForeignKey(subject => subject.SubjectTypeId)
                .IsRequired(IsSubjectTypeIdRequired);

            builder.HasOne(subject => subject.User)
                .WithMany(user => user.Subjects)
                .HasForeignKey(subject => subject.UserId)
                .IsRequired(IsUserIdRequired);

            builder.HasOne(subject => subject.Group)
                .WithMany(group => group.StaticSchedules)
                .HasForeignKey(subject => subject.GroupId)
                .IsRequired();

            builder.Property(subject => subject.Classroom)
                .HasMaxLength(ClassroomLengthMax)
                .IsRequired(IsClassroomRequired);

            builder.Property(subject => subject.Description)
                .HasMaxLength(DescriptionLengthMax)
                .IsRequired(IsDescriptionRequired);

            builder.HasMany(subject => subject.ScheduleChanges)
                .WithOne(change => change.Subject)
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
    public int? DisciplineId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.SubjectPosition" />.
    /// </summary>
    public int? SubjectPositionId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.DayPosition" />.
    /// </summary>
    public int? DayPositionId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.WeekType" />.
    /// </summary>
    public int? WeekTypeId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.SubjectType" />.
    ///     Необязательное поле.
    /// </summary>
    public int? SubjectTypeId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Security.User" />.
    ///     Необязательное поле.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Group" />.
    /// </summary>
    public int? GroupId { get; set; }

    /// <summary>
    ///     Кабинет.
    ///     Необязательное поле.
    /// </summary>
    public string? Classroom { get; set; }

    /// <summary>
    ///     Описание.
    ///     Необязательное поле.
    /// </summary>
    public string? Description { get; set; }

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Management.Discipline" />.
    /// </summary>
    public Discipline? Discipline { get; set; } 

    /// <summary>
    ///     Связанный объект <see cref="Management.SubjectPosition" />.
    /// </summary>
    public SubjectPosition? SubjectPosition { get; set; } 

    /// <summary>
    ///     Связанный объект <see cref="Management.DayPosition" />.
    /// </summary>
    public DayPosition? DayPosition { get; set; } 

    /// <summary>
    ///     Связанный объект <see cref="Management.WeekType" />.
    /// </summary>
    public WeekType? WeekType { get; set; } 

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
    public Group? Group { get; set; } 

    /// <summary>
    ///     Связанные объекты <see cref="SubjectChange" />.
    /// </summary>
    public List<SubjectChange> ScheduleChanges { get; set; } = new();
}