namespace Studenda.Server.Data.Transfer.Security;

/// <summary>
///     Тело ответа подтверждения подключения.
/// </summary>
public class HandshakeResponse
{
    /// <summary>
    ///     Роль студента.
    /// </summary>
    public required string StudentRoleName { get; init; }

    /// <summary>
    ///     Роль старосты.
    /// </summary>
    public required string LeaderRoleName { get; init; }

    /// <summary>
    ///     Роль преподавателя.
    /// </summary>
    public required string TeacherRoleName { get; init; }

    /// <summary>
    ///     Роль администратора.
    /// </summary>
    public required string AdminRoleName { get; init; }

    /// <summary>
    ///     Дата и время на сервере.
    /// </summary>
    public required DateTime CoordinatedUniversalTime { get; init; }
}