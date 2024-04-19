using ArtwiseDatabase.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ArtwiseDatabase.Entities;

/// <summary>
/// Represents a database art tag.
/// </summary>
[Comment("Represents an art tag.")]
public sealed record TagEntity : ArtwiseDbEntity
{
    /// <summary>
    /// The art to be removed.
    /// </summary>
    public ArtEntity Art { get; init; } = default!;

    /// <summary>
    /// The Id of the art.
    /// </summary>
    public required ulong ArtId { get; init; }

    /// <summary>
    /// The tag for the art.
    /// </summary>
    public required string Tag { get; init; }
}