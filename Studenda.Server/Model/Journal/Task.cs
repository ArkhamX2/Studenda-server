using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Journal.Management;
using Studenda.Server.Model.Schedule.Management;
using Studenda.Server.Model.Security;

namespace Studenda.Server.Model.Journal;

/// <summary>
///     Задание.
/// </summary>
public class Task : IdentifiableEntity
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

    public const int NameLengthMax = 32;
    public const int DescriptionLengthMax = 256;
    public const bool IsDisciplineIdRequired = true;
    public const bool IsSubjectTypeIdRequired = true;
    public const bool IsIssuerAccountIdRequired = true;
    public const bool IsAssigneeAccountIdRequired = true;
    public const bool IsMarkIdRequired = false;
    public const bool IsNameRequired = true;
    public const bool IsDescriptionRequired = false;
    public const bool IsStartedAtRequired = true;
    public const bool IsEndedAtRequired = true;

    /// <summary>
    ///     Конфигурация модели.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Task>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasOne(task => task.Discipline)
                .WithMany(discipline => discipline.Tasks)
                .HasForeignKey(task => task.DisciplineId)
                .IsRequired();

            builder.HasOne(task => task.SubjectType)
                .WithMany(type => type.Tasks)
                .HasForeignKey(task => task.SubjectTypeId)
                .IsRequired();

            builder.HasOne(task => task.IssuerAccount)
                .WithMany(account => account.IssuedTasks)
                .HasForeignKey(task => task.IssuerAccountId)
                .IsRequired();

            builder.HasOne(task => task.AssigneeAccount)
                .WithMany(account => account.AssignedTasks)
                .HasForeignKey(task => task.AssigneeAccountId)
                .IsRequired();

            builder.HasOne(task => task.Mark)
                .WithMany(mark => mark.Tasks)
                .HasForeignKey(task => task.MarkId)
                .IsRequired(IsMarkIdRequired);

            builder.Property(task => task.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(task => task.Description)
                .HasMaxLength(DescriptionLengthMax)
                .IsRequired(IsDescriptionRequired);

            builder.Property(task => task.StartedAt)
                .HasColumnType(ContextConfiguration.DateTimeType)
                .IsRequired();

            builder.Property(task => task.EndedAt)
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
    ///     Идентификатор связанного объекта <see cref="Schedule.Management.Discipline" />.
    /// </summary>
    public required int DisciplineId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Schedule.Management.SubjectType" />.
    /// </summary>
    public required int SubjectTypeId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Account" />.
    /// </summary>
    public required int IssuerAccountId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Account" />.
    /// </summary>
    public required int AssigneeAccountId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.Mark" />.
    ///     Необязательное поле.
    /// </summary>
    public int MarkId { get; set; }

    /// <summary>
    ///     Название.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Описание.
    ///     Необязательное поле.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Дата начала.
    /// </summary>
    public DateTime? StartedAt { get; set; }

    /// <summary>
    ///     Дата окончания.
    /// </summary>
    public DateTime? EndedAt { get; set; }

    #endregion

    public Discipline? Discipline { get; set; }
    public SubjectType? SubjectType { get; set; }
    public Account? IssuerAccount { get; set; }
    public Account? AssigneeAccount { get; set; }
    public Mark? Mark { get; set; }
}