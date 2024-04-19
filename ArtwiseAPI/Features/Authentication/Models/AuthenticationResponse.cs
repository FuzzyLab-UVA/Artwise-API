namespace ArtwiseAPI.Features.Authentication.Models;

/// <summary>
/// Represents a response when a user successfully authenticates with the API.
/// </summary>
/// <param name="Id">The Id of the user.</param>
/// <param name="SessionToken">The token the user must use for authenticated requests.</param>
/// <param name="ExpiresAt">The date and time the <paramref name="SessionToken"/> expires.</param>
public sealed record AuthenticationResponse(ulong Id, string SessionToken, DateTimeOffset ExpiresAt);