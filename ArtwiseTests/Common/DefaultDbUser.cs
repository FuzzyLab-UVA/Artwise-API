using ArtwiseDatabase.Entities;
using ArtwiseDatabase.Enums;
using BC = BCrypt.Net.BCrypt;

namespace ArtwiseTests.Common;

/// <summary>
/// Contains valid properties of a <see cref="UserEntity"/> object as constants,
/// so their values can be used on attributes.
/// </summary>
internal static class DefaultDbUser
{
    /// <summary>
    /// Id of the user.
    /// </summary>
    internal static Guid Id = new(GuidString);

    /// <summary>
    /// The string Id of the user.
    /// </summary>
    internal const string GuidString = "dbb75469-64b2-4272-866d-6e1937bcfa7c";

    /// <summary>
    /// E-mail of the user.
    /// </summary>
    internal const string Email = "user@email.com";

    /// <summary>
    /// Raw passwords of the user.
    /// </summary>
    internal const string Password = "avocado";

    /// <summary>
    /// The type of the user.
    /// </summary>
    internal const UserType Type = UserType.Administrator;

    /// <summary>
    /// The instance of the user.
    /// </summary>
    internal static UserEntity Instance { get; } = new()
    {
        Id = Id,
        Email = Email,
        PasswordHash = BC.HashPassword(Password),
        Type = Type,
        DateAdded = DateTimeOffset.UtcNow
    };
}