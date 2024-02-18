using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Journal.Management;
using Studenda.Core.Model.Schedule;

namespace Studenda.Core.Model.Journal;

/// <summary>
///     Оценка.
/// </summary>
public class Assessment : Identity
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

    public const bool IsAssessmentTypeIdRequired = true;
    public const bool IsTaskIdRequired = true;
    public const bool IsValueRequired = true;

    /// <summary>
    ///     Конфигурация модели.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Assessment>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.HasOne(assessment => assessment.AssessmentType)
                .WithOne(type => type.Assessment)
                .HasForeignKey<Assessment>(type => type.AssessmentTypeId);

            builder.HasOne(assessment => assessment.Task)
                .WithMany(subject => subject.Assessments)
                .HasForeignKey(assessment => assessment.TaskId)
                .IsRequired();

            builder.Property(assessment => assessment.Value)
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
    ///     Идентификатор связанного объекта <see cref="Management.AssessmentType" />.
    /// </summary>
    public required int AssessmentTypeId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Journal.Task" />.
    /// </summary>
    public required int TaskId { get; set; }

    /// <summary>
    ///     Значение.
    /// </summary>
    public required int Value { get; set; }

    #endregion

    public AssessmentType? AssessmentType { get; set; }
    public Task? Task { get; set; }
}