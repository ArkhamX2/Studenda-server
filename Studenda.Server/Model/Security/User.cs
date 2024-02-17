using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Model.Common;
using Studenda.Server.Model.Schedule;
using Studenda.Server.Model.Schedule.Management;

namespace Studenda.Server.Model.Security;

/// <summary>
///     Пользователь.
/// </summary>
public class User : IdentityUser
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
    ///     Максимальная длина поля <see cref="Surname" />.
    /// </summary>
    public const int SurnameLengthMax = 32;

    /// <summary>
    ///     Максимальная длина поля <see cref="Patronymic" />.
    /// </summary>
    public const int PatronymicLengthMax = 32;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="GroupId" />.
    /// </summary>
    public const bool IsGroupIdRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Name" />.
    /// </summary>
    public const bool IsNameRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Surname" />.
    /// </summary>
    public const bool IsSurnameRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Patronymic" />.
    /// </summary>
    public const bool IsPatronymicRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="User" />.
    /// </summary>
    internal class Configuration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(user => user.Group)
                .WithMany(group => group.Users)
                .HasForeignKey(user => user.GroupId)
                .IsRequired(IsGroupIdRequired);

            builder.Property(user => user.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired(IsNameRequired);

            builder.Property(user => user.Surname)
                .HasMaxLength(SurnameLengthMax)
                .IsRequired(IsSurnameRequired);

            builder.Property(user => user.Patronymic)
                .HasMaxLength(PatronymicLengthMax)
                .IsRequired(IsPatronymicRequired);

            builder.HasMany(user => user.Subjects)
                .WithOne(subject => subject.User)
                .HasForeignKey(subject => subject.UserId);

            builder.HasMany(user => user.SubjectChanges)
                .WithOne(change => change.User)
                .HasForeignKey(change => change.UserId);

            builder.HasMany(user => user.Disciplines)
                .WithOne(discipline => discipline.User)
                .HasForeignKey(discipline => discipline.UserId);
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
    ///     Идентификатор связанного объекта <see cref="Common.Group" />.
    ///     Необязательное поле.
    /// </summary>
    public int? GroupId { get; set; }

    /// <summary>
    ///     Имя.
    ///     Необязательное поле.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Фамилия.
    ///     Необязательное поле.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    ///     Отчество.
    ///     Необязательное поле.
    /// </summary>
    public string? Patronymic { get; set; }

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Common.Group" />.
    /// </summary>
    public Group? Group { get; set; }

    /// <summary>
    ///     Связанные объекты <see cref="Subject" />.
    /// </summary>
    public List<Subject> Subjects { get; set; } = [];

    /// <summary>
    ///     Связанные объекты <see cref="SubjectChange" />.
    /// </summary>
    public List<SubjectChange> SubjectChanges { get; set; } = [];

    /// <summary>
    ///     Связанные объекты <see cref="Discipline" />.
    /// </summary>
    public List<Discipline> Disciplines { get; set; } = [];
}