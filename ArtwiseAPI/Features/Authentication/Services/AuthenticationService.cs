using ArtwiseAPI.Common;
using ArtwiseAPI.Features.Authentication.Models;
using ArtwiseDatabase;
using ArtwiseDatabase.Entities;
using ArtwiseDatabase.Enums;
using Kotz.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace ArtwiseAPI.Features.Authentication.Services;

/// <summary>
/// Handles authentication and authorization workloads.
/// </summary>
[Service(ServiceLifetime.Scoped)]
public sealed class AuthenticationService
{
    private readonly ArtwiseDbContext _db;
    private readonly IConfiguration _config;

    /// <summary>
    /// Handles authentication and authorization workloads.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="config">The IHost configuration.</param>
    public AuthenticationService(ArtwiseDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    /// <summary>
    /// Authenticates a user.
    /// </summary>
    /// <param name="request">The controller request.</param>
    /// <returns>The authentication session token.</returns>
    /// <exception cref="ArgumentNullException">Occurs when <paramref name="request"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Occurs when the e-mail or password in the request are invalid.</exception>
    public async Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var expireAt = DateTimeOffset.UtcNow.AddMonths(3);
        var dbUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        return (dbUser is not null && BC.Verify(request.Password, dbUser.PasswordHash))
            ? new AuthenticationResponse(dbUser.Id, GenerateSessionToken(dbUser, expireAt), expireAt)
            : throw new InvalidOperationException("E-mail or password are invalid.");
    }

    /// <summary>
    /// Generates a session token for the specified user.
    /// </summary>
    /// <param name="dbUser">The user to generate the token for.</param>
    /// <param name="expiresAt">The date and time the token should expire.</param>
    /// <returns>A session token.</returns>
    /// <exception cref="ArgumentException">
    /// Occurs when <paramref name="dbUser"/> contains invalid data or when
    /// <paramref name="expiresAt"/> is less than the current time.
    /// </exception>
    public string GenerateSessionToken(UserEntity dbUser, DateTimeOffset expiresAt)
        => GenerateSessionToken(dbUser.Email, dbUser.Type, expiresAt);

    /// <summary>
    /// Generates a session token for the specified e-mail.
    /// </summary>
    /// <param name="email">The e-mail of the user.</param>
    /// <param name="userType">The type of the user.</param>
    /// <param name="expiresAt">The date and time the token should expire.</param>
    /// <returns>A session token.</returns>
    /// <exception cref="ArgumentException">
    /// Occurs when <paramref name="email"/> is <see langword="null"/> or whitespace
    /// or when <paramref name="expiresAt"/> is less than the current time.
    /// </exception>
    public string GenerateSessionToken(string email, UserType userType, DateTimeOffset expiresAt)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        if (expiresAt <= DateTimeOffset.UtcNow)
            throw new ArgumentException("Token must expire in the future.", nameof(expiresAt));

        // Generate the claims (for authorization)
        var claims = new Claim[]
        {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, userType.ToString())
        };

        // Generate the token
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = new(new SymmetricSecurityKey(_config.GetValue<byte[]>(ApiConstants.JwtAppSetting)), SecurityAlgorithms.HmacSha256Signature),
            Expires = expiresAt.DateTime,
            Subject = new(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}