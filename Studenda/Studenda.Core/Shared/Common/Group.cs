using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Model.Data.Configuration;
using Studenda.Model.Shared.Link;

namespace Studenda.Model.Shared.Common;

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
        public Configuration(DatabaseConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(group => group.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.HasOne(group => group.Course)
                .WithMany(course => course.Groups)
                .HasForeignKey(group => group.CourseId)
                .IsRequired();

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
     *
     * Константы, задающие базовые конфигурации полей
     * и ограничения модели.
     */
    #region Configuration

    /// <summary>
    /// Минимальная длина поля <see cref="Name"/>.
    /// </summary>
    public const int NameLengthMin = 1;

    /// <summary>
    /// Максимальная длина поля <see cref="Name"/>.
    /// </summary>
    public const int NameLengthMax = 128;

    #endregion

    /*             _   _ _
     *   ___ _ __ | |_(_) |_ _   _
     *  / _ \ '_ \| __| | __| | | |
     * |  __/ | | | |_| | |_| |_| |
     *  \___|_| |_|\__|_|\__|\__, |
     *                       |___/
     *
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
    /// Связанный объект <see cref="UserGroupLink"/>.
    /// </summary>
    public List<UserGroupLink> UserGroupLinks { get; set; } = null!;
}
