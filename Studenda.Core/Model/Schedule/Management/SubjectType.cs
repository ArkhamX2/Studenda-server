using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Schedule.Management;

/// <summary>
///     Тип занятия.
/// </summary>
public class SubjectType : Identity
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
    ///     Значение по умолчанию для поля <see cref="IsScorable" />.
    /// </summary>
    public const bool IsScorableDefaultValue = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Name" />.
    /// </summary>
    public const bool IsNameRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="IsScorable" />.
    /// </summary>
    public const bool IsScorableRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="SubjectType" />.
    /// </summary>
    internal class Configuration : Configuration<SubjectType>
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
        public override void Configure(EntityTypeBuilder<SubjectType> builder)
        {
            builder.Property(type => type.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(type => type.IsScorable)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasMany(type => type.Subjects)
                .WithOne(subject => subject.SubjectType)
                .HasForeignKey(subject => subject.SubjectTypeId);

            builder.HasMany(type => type.SubjectChanges)
                .WithOne(change => change.SubjectType)
                .HasForeignKey(change => change.SubjectTypeId);

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
    ///     Название.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Статус оцениваемости.
    /// </summary>
    public bool IsScorable { get; set; }

    #endregion

    /// <summary>
    ///     Связанные объекты <see cref="Subject" />.
    /// </summary>
    public List<Subject> Subjects { get; set; } = null!;

    /// <summary>
    ///     Связанные объекты <see cref="SubjectChange" />.
    /// </summary>
    public List<SubjectChange> SubjectChanges { get; set; } = null!;
}