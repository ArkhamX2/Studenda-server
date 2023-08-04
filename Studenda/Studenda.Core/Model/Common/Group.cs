using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Link;

namespace Studenda.Core.Model.Common;

/// <summary>
/// Группа.
/// </summary>
public class Group : Entity
{
    /// <summary>
    /// Конфигурация модели <see cref="Group"/>.
    /// </summary>
    internal class Configuration : Configuration<Group>
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
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(group => group.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired(IsNameRequired);

            builder.HasOne(group => group.Course)
                .WithMany(course => course.Groups)
                .HasForeignKey(group => group.CourseId)
                .IsRequired(IsCourseIdRequired);

            builder.HasMany(group => group.UserGroupLinks)
                .WithOne(link => link.Group)
                .HasForeignKey(link => link.GroupId);

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
    /// Статус необходимости наличия значения в поле <see cref="CourseId"/>.
    /// </summary>
    public const bool IsCourseIdRequired = true;

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
    /// Идентификатор связанного объекта <see cref="Common.Course"/>.
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;

    #endregion

    /// <summary>
    /// Связанный объект <see cref="Common.Course"/>.
    /// </summary>
    public Course Course { get; set; } = null!;

    /// <summary>
    /// Связанные объекты <see cref="UserGroupLink"/>.
    /// </summary>
    public List<UserGroupLink> UserGroupLinks { get; set; } = null!;
}
