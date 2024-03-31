namespace ArtwiseDatabase.Entities.Abstractions;

/// <summary>
/// Represents a database table.
/// </summary>
public abstract record ArtwiseDbEntity
{
    /// <summary>
    /// Date and time of when this entity was added to the database.
    /// </summary>
    public DateTimeOffset DateAdded { get; init; } = DateTimeOffset.UtcNow;
}