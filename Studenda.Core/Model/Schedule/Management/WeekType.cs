using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Schedule.Management;

/// <summary>
///     Тип учебной недели.
/// </summary>
public class WeekType : Identity
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
    public const int NameLengthMax = 64;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Index" />.
    /// </summary>
    public const bool IsIndexRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Name" />.
    /// </summary>
    public const bool IsNameRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="WeekType" />.
    /// </summary>
    internal class Configuration : Configuration<WeekType>
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
        public override void Configure(EntityTypeBuilder<WeekType> builder)
        {
            builder.Property(type => type.Index)
                .IsRequired();

            builder.Property(type => type.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired(IsNameRequired);

            builder.HasMany(type => type.StaticSchedules)
                .WithOne(schedule => schedule.WeekType)
                .HasForeignKey(schedule => schedule.WeekTypeId);

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
    ///     Индекс относительно начала учебного года.
    /// </summary>
    public required int Index { get; set; }

    /// <summary>
    ///     Название.
    ///     Необязательное поле.
    /// </summary>
    public string? Name { get; set; }

    #endregion

    /// <summary>
    ///     Связанные объекты <see cref="Subject" />.
    /// </summary>
    public List<Subject> StaticSchedules { get; set; } = new();
}