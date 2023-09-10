using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Schedule;

/// <summary>
///     Занятие.
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
    ///     Максимальная длина поля <see cref="Name" />.
    /// </summary>
    public const int NameLengthMax = 32;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="UserId" />.
    /// </summary>
    public const bool IsUserIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Name" />.
    /// </summary>
    public const bool IsNameRequired = true;

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
            builder.HasOne(subject => subject.User)
                .WithMany(user => user.Subjects)
                .HasForeignKey(user => user.UserId)
                .IsRequired();

            builder.Property(type => type.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.HasMany(subject => subject.StaticSchedules)
                .WithOne(schedule => schedule.Subject)
                .HasForeignKey(schedule => schedule.SubjectId);

            builder.HasMany(subject => subject.ScheduleChanges)
                .WithOne(change => change.Subject)
                .HasForeignKey(change => change.SubjectId);

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
    public int UserId { get; set; }

    /// <summary>
    ///     Название.
    /// </summary>
    public string Name { get; set; } = null!;

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Security.User" />.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    ///     Связанные объекты <see cref="StaticSchedule" />.
    /// </summary>
    public List<StaticSchedule> StaticSchedules { get; set; } = null!;

    /// <summary>
    ///     Связанные объекты <see cref="ScheduleChange" />.
    /// </summary>
    public List<ScheduleChange> ScheduleChanges { get; set; } = null!;
}