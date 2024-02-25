using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Common;
using Studenda.Server.Model.Journal;
using Studenda.Server.Model.Schedule;
using Studenda.Server.Model.Schedule.Management;
using Studenda.Server.Model.Security.Management;
using Task = Studenda.Server.Model.Journal.Task;

namespace Studenda.Server.Model.Security;

/// <summary>
///     Пользователь.
/// </summary>
public class User : IdentifiableEntity
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

    public const int IdentityIdLengthMax = 128;
    public const int NameLengthMax = 32;
    public const int SurnameLengthMax = 32;
    public const int PatronymicLengthMax = 32;
    public const bool IsIdentityIdRequired = false;
    public const bool IsRoleIdRequired = true;
    public const bool IsGroupIdRequired = false;
    public const bool IsNameRequired = false;
    public const bool IsSurnameRequired = false;
    public const bool IsPatronymicRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="User" />.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    internal class Configuration(ContextConfiguration configuration) : Configuration<User>(configuration)
    {
        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .IsRequired();

            builder.HasOne(user => user.Group)
                .WithMany(group => group.Users)
                .HasForeignKey(user => user.GroupId)
                .IsRequired(IsGroupIdRequired);

            builder.Property(user => user.IdentityId)
                .HasMaxLength(IdentityIdLengthMax)
                .IsRequired(IsIdentityIdRequired);

            builder.Property(user => user.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired(IsNameRequired);

            builder.Property(user => user.Surname)
                .HasMaxLength(SurnameLengthMax)
                .IsRequired(IsSurnameRequired);

            builder.Property(user => user.Patronymic)
                .HasMaxLength(PatronymicLengthMax)
                .IsRequired(IsPatronymicRequired);

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
    ///     Идентификатор связанного объекта <see cref="Management.Role" />.
    /// </summary>
    public int? RoleId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Group" />.
    ///     Необязательное поле.
    /// </summary>
    public int? GroupId { get; set; }

    /// <summary>
    ///     Идентификатор в системе безопасности.
    ///     Необязательное поле.
    /// </summary>
    public string? IdentityId { get; set; }

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

    public Role? Role { get; set; }
    public Group? Group { get; set; }
    public List<Subject> Subjects { get; set; } = [];
    public List<SubjectChange> SubjectChanges { get; set; } = [];
    public List<Discipline> Disciplines { get; set; } = [];
    public List<Absence> Absences { get; set; } = [];
    public List<Task> IssuedTasks { get; set; } = [];
    public List<Task> AssignedTasks { get; set; } = [];
}