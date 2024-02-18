using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Schedule;

/// <summary>
///     Замена статичного занятия.
///     Позволяет подменить данные у конкретного занятия.
/// </summary>
public class SubjectChange : IdentifiableEntity
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
    public const bool IsStaticScheduleIdRequired = true;
    public const bool IsDisciplineIdRequired = false;
    public const bool IsSubjectTypeIdRequired = false;
    public const bool IsUserIdRequired = false;
    public const bool IsClassroomRequired = false;
    public const bool IsDescriptionRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="SubjectChange" />.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<SubjectChange>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<SubjectChange> builder)
        {
            builder.HasOne(change => change.Subject)
                .WithMany(subject => subject.ScheduleChanges)
                .HasForeignKey(change => change.StaticScheduleId)
                .IsRequired();

            builder.HasOne(change => change.Discipline)
                .WithMany(discipline => discipline.SubjectChanges)
                .HasForeignKey(change => change.DisciplineId)
                .IsRequired(IsDisciplineIdRequired);

            builder.HasOne(change => change.SubjectType)
                .WithMany(type => type.SubjectChanges)
                .HasForeignKey(change => change.SubjectTypeId)
                .IsRequired(IsSubjectTypeIdRequired);

            builder.HasOne(change => change.User)
                .WithMany(user => user.SubjectChanges)
                .HasForeignKey(change => change.UserId)
                .IsRequired(IsUserIdRequired);

            builder.Property(subject => subject.Classroom)
                .HasMaxLength(ClassroomLengthMax)
                .IsRequired(IsClassroomRequired);

            builder.Property(change => change.Description)
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
    ///     Идентификатор связанного объекта <see cref="Subject" />.
    /// </summary>
    public int? StaticScheduleId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.Discipline" />.
    ///     Необязательное поле.
    /// </summary>
    public int? DisciplineId { get; set; }

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

    public Subject? Subject { get; set; }
    public Discipline? Discipline { get; set; }
    public SubjectType? SubjectType { get; set; }
    public User? User { get; set; }
}