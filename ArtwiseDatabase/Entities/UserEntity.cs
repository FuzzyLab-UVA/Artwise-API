using ArtwiseDatabase.Entities.Abstractions;
using ArtwiseDatabase.Enums;
using Microsoft.EntityFrameworkCore;

namespace ArtwiseDatabase.Entities;

/// <summary>
/// Represents a database user.
/// </summary>
[Comment("Represents a user.")]
public sealed record UserEntity : ArtwiseDbEntity
{
    /// <summary>
    /// The Id of the user.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// The e-mail of the user.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The password hash of the user.
    /// </summary>
    public required string PasswordHash { get; init; }

    /// <summary>
    /// The type of the user.
    /// </summary>
    public UserType Type { get; init; } = UserType.User;
}