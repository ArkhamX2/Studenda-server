namespace Studenda.Server.Data.Transfer.Security;

/// <summary>
///     Тело ответа подтверждения подключения.
/// </summary>
public class HandshakeResponse
{
    /// <summary>
    ///     Доступ студента.
    /// </summary>
    public required string DefaultPermission { get; init; }

    /// <summary>
    ///     Доступ старосты.
    /// </summary>
    public required string LeaderPermission { get; init; }

    /// <summary>
    ///     Доступ преподавателя.
    /// </summary>
    public required string TeacherPermission { get; init; }

    /// <summary>
    ///     Доступ администратора.
    /// </summary>
    public required string AdminPermission { get; init; }

    /// <summary>
    ///     Дата и время на сервере.
    /// </summary>
    public required DateTime CoordinatedUniversalTime { get; init; }
}