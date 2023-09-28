using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Schedule.Management;

/// <summary>
///     Учебная дисциплина.
/// </summary>
public class Discipline : Identity
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
    ///     Максимальная длина поля <see cref="Description" />.
    /// </summary>
    public const int DescriptionLengthMax = 32;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="UserId" />.
    /// </summary>
    public const bool IsUserIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Name" />.
    /// </summary>
    public const bool IsNameRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Description" />.
    /// </summary>
    public const bool IsDescriptionRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="Discipline" />.
    /// </summary>
    internal class Configuration : Configuration<Discipline>
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
        public override void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasOne(discipline => discipline.User)
                .WithMany(user => user.Disciplines)
                .HasForeignKey(discipline => discipline.UserId)
                .IsRequired();

            builder.Property(discipline => discipline.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(discipline => discipline.Description)
                .HasMaxLength(DescriptionLengthMax)
                .IsRequired(IsDescriptionRequired);

            builder.HasMany(discipline => discipline.Subjects)
                .WithOne(subject => subject.Discipline)
                .HasForeignKey(subject => subject.DisciplineId);

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

    /// <summary>
    ///     Описание.
    ///     Необязательное поле.
    /// </summary>
    public string? Description { get; set; }

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Security.User" />.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    ///     Связанные объекты <see cref="Subject" />.
    /// </summary>
    public List<Subject> Subjects { get; set; } = null!;
}