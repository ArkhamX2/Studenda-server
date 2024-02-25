using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Schedule;
using Studenda.Server.Model.Security;

namespace Studenda.Server.Model.Common;

/// <summary>
///     Группа.
/// </summary>
public class Group : IdentifiableEntity
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

    public const int NameLengthMax = 128;
    public const bool IsCourseIdRequired = true;
    public const bool IsDepartmentIdRequired = true;
    public const bool IsNameRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="Group" />.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Group>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasOne(group => group.Course)
                .WithMany(course => course.Groups)
                .HasForeignKey(group => group.CourseId)
                .IsRequired();

            builder.HasOne(group => group.Department)
                .WithMany(department => department.Groups)
                .HasForeignKey(group => group.DepartmentId)
                .IsRequired();

            builder.Property(group => group.Name)
                .HasMaxLength(NameLengthMax)
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
    ///     Идентификатор связанного объекта <see cref="Common.Course" />.
    /// </summary>
    public int? CourseId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Department" />.
    /// </summary>
    public int? DepartmentId { get; set; }

    /// <summary>
    ///     Название.
    /// </summary>
    public required string Name { get; set; }

    #endregion

    public Course? Course { get; set; } 
    public Department? Department { get; set; } 
    public List<User> Users { get; set; } = [];
    public List<Subject> StaticSchedules { get; set; } = [];
}