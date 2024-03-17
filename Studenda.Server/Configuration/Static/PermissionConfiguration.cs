namespace Studenda.Server.Configuration.Static;

/// <summary>
///     Статичная конфигурация доступов.
/// </summary>
public static class PermissionConfiguration
{
    /// <summary>
    ///     Идентификатор доступа по-умолчанию.
    /// </summary>
    public const string DefaultPermission = "ru.arkham.permission.default";

    /// <summary>
    ///     Идентификатор доступа старосты.
    /// </summary>
    public const string LeaderPermission = "ru.arkham.permission.leader";

    /// <summary>
    ///     Идентификатор доступа преподавателя.
    /// </summary>
    public const string TeacherPermission = "ru.arkham.permission.teacher";

    /// <summary>
    ///     Идентификатор доступа администратора.
    /// </summary>
    public const string AdminPermission = "ru.arkham.permission.admin";

    /// <summary>
    ///     Получить доступные идентификаторы доступа.
    /// </summary>
    /// <returns>Список идентификаторов доступа.</returns>
    public static List<string> GetPermissions()
    {
        return
        [
            DefaultPermission,
            LeaderPermission,
            TeacherPermission,
            AdminPermission
        ];
    }
}