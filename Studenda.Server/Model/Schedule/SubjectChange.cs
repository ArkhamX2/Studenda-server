using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Schedule.Management;
using Studenda.Server.Model.Security;

namespace Studenda.Server.Model.Schedule;

/// <summary>
///     Замена статичного занятия.
///     Позволяет подменить данные у конкретного занятия.
/// </summary>
public class SubjectChange : Identity
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
    ///     Статус необходимости наличия значения в поле <see cref="StaticScheduleId" />.
    /// </summary>
    public const bool IsStaticScheduleIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="DisciplineId" />.
    /// </summary>
    public const bool IsDisciplineIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="SubjectTypeId" />.
    /// </summary>
    public const bool IsSubjectTypeIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="UserId" />.
    /// </summary>
    public const bool IsUserIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Classroom" />.
    /// </summary>
    public const bool IsClassroomRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Description" />.
    /// </summary>
    public const bool IsDescriptionRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="SubjectChange" />.
    /// </summary>
    internal class Configuration : Configuration<SubjectChange>
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

    /// <summary>
    ///     Связанный объект <see cref="Subject" />.
    /// </summary>
    public Subject? Subject { get; set; }

    /// <summary>
    ///     Связанный объект <see cref="Management.Discipline" />.
    /// </summary>
    public Discipline? Discipline { get; set; }

    /// <summary>
    ///     Связанный объект <see cref="Management.SubjectType" />.
    /// </summary>
    public SubjectType? SubjectType { get; set; }

    /// <summary>
    ///     Связанный объект <see cref="Security.User" />.
    /// </summary>
    public User? User { get; set; }
}