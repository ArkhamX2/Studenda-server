using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Schedule.Management;

public class SubjectPosition : Identity
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
    ///     Максимальная длина поля <see cref="StartLabel" />.
    /// </summary>
    public const int StartLabelLengthMax = 32;

    /// <summary>
    ///     Максимальная длина поля <see cref="EndLabel" />.
    /// </summary>
    public const int EndLabelLengthMax = 32;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="StartLabel" />.
    /// </summary>
    public const bool IsStartLabelRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="EndLabel" />.
    /// </summary>
    public const bool IsEndLabelRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="SubjectPosition" />.
    /// </summary>
    internal class Configuration : Configuration<SubjectPosition>
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
        public override void Configure(EntityTypeBuilder<SubjectPosition> builder)
        {
            builder.Property(type => type.StartLabel)
                .HasMaxLength(StartLabelLengthMax)
                .IsRequired();

            builder.Property(type => type.EndLabel)
                .HasMaxLength(EndLabelLengthMax)
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
    ///     Обозначение начала.
    /// </summary>
    public string StartLabel { get; set; } = null!;

    /// <summary>
    ///     Обозначение окончания.
    /// </summary>
    public string EndLabel { get; set; } = null!;

    #endregion
}