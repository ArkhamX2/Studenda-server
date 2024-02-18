namespace Studenda.Core.Model;

/// <summary>
///     Модель с уникальным идентификатором.
/// </summary>
public class Identity : Entity
{
    /// <summary>
    ///     Идентификатор.
    /// </summary>
    public int? Id { get; set; }
}