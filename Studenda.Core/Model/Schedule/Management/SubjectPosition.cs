using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model.Schedule.Management;

/// <summary>
///     Позиция занятия в учебном дне.
/// </summary>
public class SubjectPosition : Identity
{
    /// <summary>
    ///     Начальное значение индекса.
    /// </summary>
    public const int StartIndex = 0;

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
    ///     Максимальная длина поля <see cref="Name" />.
    /// </summary>
    public const int NameLengthMax = 64;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Index" />.
    /// </summary>
    public const bool IsIndexRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="StartLabel" />.
    /// </summary>
    public const bool IsStartLabelRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="EndLabel" />.
    /// </summary>
    public const bool IsEndLabelRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Name" />.
    /// </summary>
    public const bool IsNameRequired = false;

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
            builder.Property(position => position.Index)
                .IsRequired();

            builder.Property(position => position.StartLabel)
                .HasMaxLength(StartLabelLengthMax)
                .IsRequired(IsStartLabelRequired);

            builder.Property(position => position.EndLabel)
                .HasMaxLength(EndLabelLengthMax)
                .IsRequired(IsEndLabelRequired);

            builder.Property(position => position.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired(IsNameRequired);

            builder.HasMany(position => position.StaticSchedules)
                .WithOne(schedule => schedule.SubjectPosition)
                .HasForeignKey(schedule => schedule.SubjectPositionId);

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
    ///     Индекс в учебном дне.
    /// </summary>
    public required int Index { get; set; }

    /// <summary>
    ///     Обозначение начала.
    ///     Необязательное поле.
    /// </summary>
    public string? StartLabel { get; set; }

    /// <summary>
    ///     Обозначение окончания.
    ///     Необязательное поле.
    /// </summary>
    public string? EndLabel { get; set; }

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