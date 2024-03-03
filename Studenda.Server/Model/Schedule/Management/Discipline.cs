using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Common;
using Task = Studenda.Server.Model.Journal.Task;

namespace Studenda.Server.Model.Schedule.Management;

/// <summary>
///     Учебная дисциплина.
/// </summary>
public class Discipline : IdentifiableEntity
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
    public const int DescriptionLengthMax = 32;
    public const bool IsAccountIdRequired = true;
    public const bool IsNameRequired = true;
    public const bool IsDescriptionRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="Discipline" />.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Discipline>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasOne(discipline => discipline.Account)
                .WithMany(account => account.Disciplines)
                .HasForeignKey(discipline => discipline.AccountId)
                .IsRequired();

            builder.Property(discipline => discipline.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.Property(discipline => discipline.Description)
                .HasMaxLength(DescriptionLengthMax)
                .IsRequired(IsDescriptionRequired);

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
    ///     Идентификатор связанного объекта <see cref="Common.Account" />.
    /// </summary>
    public int? AccountId { get; set; }

    /// <summary>
    ///     Название.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Описание.
    ///     Необязательное поле.
    /// </summary>
    public string? Description { get; set; }

    #endregion

    public Account? Account { get; set; }
    public List<Subject> Subjects { get; set; } = [];
    public List<SubjectChange> SubjectChanges { get; set; } = [];
    public List<Task> Tasks { get; set; } = [];
}