using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Journal;

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
    public const bool IsIssuerUserIdRequired = true;
    public const bool IsAssigneeUserIdRequired = true;
    public const bool IsNameRequired = true;
    public const bool IsDescriptionRequired = false;
    public const bool IsEndsAtRequired = true;

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

            builder.HasOne(task => task.IssuerUser)
                .WithMany(user => user.IssuedTasks)
                .HasForeignKey(task => task.IssuerUserId)
                .IsRequired();

            builder.HasOne(task => task.AssigneeUser)
                .WithMany(user => user.AssignedTasks)
                .HasForeignKey(task => task.AssigneeUserId)
                .IsRequired();

            builder.Property(task => task.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(task => task.Description)
                .HasMaxLength(DescriptionLengthMax)
                .IsRequired(IsDescriptionRequired);

            builder.Property(task => task.EndsAt)
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
    ///     Идентификатор связанного объекта <see cref="Management.Discipline" />.
    /// </summary>
    public required int DisciplineId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Management.SubjectType" />.
    /// </summary>
    public required int SubjectTypeId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="User" />.
    /// </summary>
    public required int IssuerUserId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="User" />.
    /// </summary>
    public required int AssigneeUserId { get; set; }

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
    ///     Дата закрытия.
    /// </summary>
    public DateTime? EndsAt { get; set; }

    #endregion

    public Discipline? Discipline { get; set; }
    public SubjectType? SubjectType { get; set; }
    public User? IssuerUser { get; set; }
    public User? AssigneeUser { get; set; }
    public List<Mark> Marks { get; set; } = [];
}