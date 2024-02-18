using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Schedule.Management;

/// <summary>
///     Тип учебной недели.
/// </summary>
public class WeekType : Identity
{
    /// <summary>
    ///     Начальное значение индекса.
    /// </summary>
    public const int StartIndex = 1;

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

    public const int NameLengthMax = 64;
    public const bool IsIndexRequired = true;
    public const bool IsNameRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="WeekType" />.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<WeekType>(configuration)
    {
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

    public List<Subject> StaticSchedules { get; set; } = [];
}