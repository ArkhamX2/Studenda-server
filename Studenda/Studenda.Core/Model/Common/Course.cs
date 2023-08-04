using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Common;

/// <summary>
/// Курс.
/// </summary>
public class Course : Entity
{
    /// <summary>
    /// Конфигурация модели <see cref="Course"/>.
    /// </summary>
    internal class Configuration : Configuration<Course>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        public Configuration(ContextConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(course => course.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired(IsNameRequired);

            builder.HasOne(course => course.Department)
                .WithMany(department => department.Courses)
                .HasForeignKey(course => course.DepartmentId)
                .IsRequired(IsDepartmentIdRequired);

            builder.HasMany(course => course.Groups)
                .WithOne(group => group.Course)
                .HasForeignKey(group => group.CourseId);

            base.Configure(builder);
        }
    }
    
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
    /// Максимальная длина поля <see cref="Name"/>.
    /// </summary>
    public const int NameLengthMax = 128;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="DepartmentId"/>.
    /// </summary>
    public const bool IsDepartmentIdRequired = true;

    /// <summary>
    /// Статус необходимости наличия значения в поле <see cref="Name"/>.
    /// </summary>
    public const bool IsNameRequired = true;

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
    /// Идентификатор связанного объекта <see cref="Common.Department"/>.
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;

    #endregion

    /// <summary>
    /// Связанный объект <see cref="Common.Department"/>.
    /// </summary>
    public Department Department { get; set; } = null!;

    /// <summary>
    /// Связанные объекты <see cref="Group"/>.
    /// </summary>
    public List<Group> Groups { get; set; } = null!;
}
