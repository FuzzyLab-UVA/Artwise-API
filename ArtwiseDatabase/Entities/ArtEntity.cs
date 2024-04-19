using ArtwiseDatabase.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ArtwiseDatabase.Entities;

/// <summary>
/// Represents a database art.
/// </summary>
[Comment("Represents an art.")]
public sealed record ArtEntity : ArtwiseDbEntity
{
    /// <summary>
    /// The removal requests associated with this art.
    /// </summary>
    public ICollection<RemovalRequestEntity> RemovalRequests { get; init; } = [];

    /// <summary>
    /// The tags associated with this art.
    /// </summary>
    public ICollection<TagEntity> Tags { get; init; } = [];

    /// <summary>
    /// The Id of the art.
    /// </summary>
    public ulong Id { get; init; }

    /// <summary>
    /// The author of the art.
    /// </summary>
    public required string Author { get; init; }

    /// <summary>
    /// The direct URL to the art. 
    /// </summary>
    public required string ImageUrl { get; init; }

    /// <summary>
    /// The user-facing page where the art was published.
    /// </summary>
    public required string SourceUrl { get; init; }

    /// <summary>
    /// Determines whether the art was made by an AI or not.
    /// </summary>
    public required bool AuthorIsAI { get; init; }

    /// <summary>
    /// Determines whether the art is currently accessible or not.
    /// </summary>
    public required bool IsAccessible { get; init; }
}