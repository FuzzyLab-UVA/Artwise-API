using ArtwiseDatabase.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ArtwiseDatabase.Entities;

/// <summary>
/// Represents a database a user art addition request.
/// </summary>
[Comment("Represents a user art addition request.")]
public sealed record AdditionRequestEntity : ArtwiseDbEntity
{
    /// <summary>
    /// The Id of the request.
    /// </summary>
    public int Id { get; init; }

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
    /// The suggested tags for the image, separated by semi-colons. 
    /// </summary>
    public required string? Tags { get; init; }
}