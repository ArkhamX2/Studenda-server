using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Security;

namespace Studenda.Server.Model.Journal;

/// <summary>
///     Прогул.
/// </summary>
public class Absence : IdentifiableEntity
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

    public const bool IsAccountIdRequired = true;
    public const bool IsSessionIdRequired = true;

    /// <summary>
    ///     Конфигурация модели.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<Absence>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Absence> builder)
        {
            builder.HasOne(absence => absence.Session)
                .WithMany(session => session.Absences)
                .HasForeignKey(absence => absence.SessionId)
                .IsRequired();

            builder.HasOne(absence => absence.Account)
                .WithMany(account => account.Absences)
                .HasForeignKey(absence => absence.AccountId)
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
    ///     Идентификатор связанного объекта <see cref="Security.Account" />.
    /// </summary>
    public required int AccountId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Journal.Session" />.
    /// </summary>
    public required int SessionId { get; set; }

    #endregion

    public Account? Account { get; set; }
    public Session? Session { get; set; }
}