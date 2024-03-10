namespace Studenda.Server.Configuration.Static;

/// <summary>
///     Конфигурация ролей.
/// </summary>
public static class IdentityRoleConfiguration
{
    /// <summary>
    ///     Роль студента.
    /// </summary>
    public const string StudentRoleName = "Student";

    /// <summary>
    ///     Роль старосты.
    /// </summary>
    public const string LeaderRoleName = "Leader";

    /// <summary>
    ///     Роль преподавателя.
    /// </summary>
    public const string TeacherRoleName = "Teacher";

    /// <summary>
    ///     Роль администратора.
    /// </summary>
    public const string AdminRoleName = "Admin";

    /// <summary>
    ///     Получить доступные названия ролей.
    /// </summary>
    /// <returns>Список названий ролей.</returns>
    public static List<string> GetRoleNames()
    {
        return
        [
            StudentRoleName,
            LeaderRoleName,
            TeacherRoleName,
            AdminRoleName
        ];
    }
}