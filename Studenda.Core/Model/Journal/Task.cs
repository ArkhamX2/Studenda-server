using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Model.Security;
using Group = Studenda.Core.Model.Common.Group;

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
    public const bool IsGroupIdRequired = true;
    public const bool IsUserIdRequired = true;
    public const bool IsNameRequired = true;
    public const bool IsDescriptionRequired = false;

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

            builder.HasOne(task => task.Group)
                .WithMany(group => group.Tasks)
                .HasForeignKey(task => task.GroupId)
                .IsRequired();

            builder.HasOne(task => task.User)
                .WithMany(user => user.Tasks)
                .HasForeignKey(task => task.UserId)
                .IsRequired();

            builder.Property(user => user.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(user => user.Description)
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
    ///     Идентификатор связанного объекта <see cref="Schedule.Management.Discipline" />.
    /// </summary>
    public required int DisciplineId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Schedule.Management.SubjectType" />.
    /// </summary>
    public required int SubjectTypeId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Group" />.
    /// </summary>
    public required int GroupId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Security.User" />.
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    ///     Название.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Описание.
    ///     Необязательное поле.
    /// </summary>
    public string? Description { get; set; }

    #endregion

    public Discipline? Discipline { get; set; }
    public SubjectType? SubjectType { get; set; }
    public Group? Group { get; set; }
    public User? User { get; set; }
    public List<Assessment> Assessments { get; set; } = [];
}