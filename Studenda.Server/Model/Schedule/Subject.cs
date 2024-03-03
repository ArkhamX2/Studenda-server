using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Common;
using Studenda.Server.Model.Journal;
using Studenda.Server.Model.Schedule.Management;

namespace Studenda.Server.Model.Schedule;

/// <summary>
///     Статичное занятие на определенный день
///     для определенной группы.
/// </summary>
public class Subject : IdentifiableEntity
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

    public const int ClassroomLengthMax = 32;
    public const int DescriptionLengthMax = 256;
    public const bool IsDisciplineIdRequired = true;
    public const bool IsSubjectPositionIdRequired = true;
    public const bool IsDayPositionIdRequired = true;
    public const bool IsWeekTypeIdRequired = true;
    public const bool IsSubjectTypeIdRequired = false;
    public const bool IsAccountIdRequired = false;
    public const bool IsGroupIdRequired = true;
    public const bool IsAcademicYearRequired = true;
    public const bool IsClassroomRequired = false;
    public const bool IsDescriptionRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="Subject" />.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Subject>(configuration)
    {
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

            builder.HasOne(subject => subject.Account)
                .WithMany(account => account.Subjects)
                .HasForeignKey(subject => subject.AccountId)
                .IsRequired(IsAccountIdRequired);

            builder.HasOne(subject => subject.Group)
                .WithMany(group => group.StaticSchedules)
                .HasForeignKey(subject => subject.GroupId)
                .IsRequired();

            builder.Property(subject => subject.AcademicYear)
                .IsRequired();

            builder.Property(subject => subject.Classroom)
                .HasMaxLength(ClassroomLengthMax)
                .IsRequired(IsClassroomRequired);

            builder.Property(subject => subject.Description)
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
    ///     Идентификатор связанного объекта <see cref="Common.Account" />.
    ///     Необязательное поле.
    /// </summary>
    public int? AccountId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Group" />.
    /// </summary>
    public int? GroupId { get; set; }

    /// <summary>
    ///     Учебный год.
    /// </summary>
    public required int AcademicYear { get; set; }

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

    public Discipline? Discipline { get; set; }
    public SubjectPosition? SubjectPosition { get; set; }
    public DayPosition? DayPosition { get; set; }
    public WeekType? WeekType { get; set; }
    public SubjectType? SubjectType { get; set; }
    public Account? Account { get; set; }
    public Group? Group { get; set; }
    public List<SubjectChange> ScheduleChanges { get; set; } = [];
    public List<Absence> Absences { get; set; } = [];
    public List<Session> Sessions { get; set; } = [];
}