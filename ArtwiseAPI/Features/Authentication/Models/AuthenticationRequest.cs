using ArtwiseAPI.Common;
using System.ComponentModel.DataAnnotations;

namespace ArtwiseAPI.Features.Authentication.Models;

/// <summary>
/// Represents a request for user authentication.
/// </summary>
/// <param name="Email">The e-mail of the user.</param>
/// <param name="Password">The password of the user.</param>
public sealed record AuthenticationRequest(string Email, string Password) : IValidatableObject
{
    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!ApiStatics.EmailRegex.IsMatch(Email ?? string.Empty))
            yield return new ValidationResult("E-mail is not valid.", [nameof(Email)]);

        if (string.IsNullOrWhiteSpace(Password))
            yield return new ValidationResult("Password cannot be null, empty, or whitespace.", [nameof(Password)]);
    }
}