namespace Studenda.Server.Model;

/// <summary>
///     Модель <see cref="Entity" /> с уникальным идентификатором.
/// </summary>
public class Identity : Entity
{
    /// <summary>
    ///     Идентификатор.
    /// </summary>
    public int? Id { get; set; }
}