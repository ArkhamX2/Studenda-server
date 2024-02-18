using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Task = Studenda.Core.Model.Journal.Task;

namespace Studenda.Core.Model.Schedule.Management;

/// <summary>
///     Тип занятия.
/// </summary>
public class SubjectType : IdentifiableEntity
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
    public const bool IsScorableDefaultValue = false;
    public const bool IsNameRequired = true;
    public const bool IsScorableRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="SubjectType" />.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<SubjectType>(configuration)
    {
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
    public required string Name { get; set; }

    /// <summary>
    ///     Статус оцениваемости.
    /// </summary>
    public required bool IsScorable { get; set; }

    #endregion

    public List<Subject> Subjects { get; set; } = [];
    public List<SubjectChange> SubjectChanges { get; set; } = [];
    public List<Task> Tasks { get; set; } = [];
}