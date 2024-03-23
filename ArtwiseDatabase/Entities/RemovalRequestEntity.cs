using ArtwiseDatabase.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ArtwiseDatabase.Entities;

/// <summary>
/// Represents a database a user art removal request.
/// </summary>
[Comment("Represents a user art removal request.")]
public sealed record RemovalRequestEntity : ArtwiseDbEntity
{
    /// <summary>
    /// The art to be removed.
    /// </summary>
    public ArtEntity Art { get; init; } = default!;

    /// <summary>
    /// The Id of the art this request is associated with.
    /// </summary>
    public required Guid ArtId { get; init; }

    /// <summary>
    /// The e-mail of the user.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The request provided by the user.
    /// </summary>
    public required string Description { get; init; }
}