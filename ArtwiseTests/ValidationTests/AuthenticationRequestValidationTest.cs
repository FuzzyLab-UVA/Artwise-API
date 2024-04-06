using ArtwiseAPI.Features.Authentication.Models;
using System.ComponentModel.DataAnnotations;

namespace ArtwiseTests.ValidationTests;

public sealed class AuthenticationRequestValidationTest
{
    [Theory]
    [InlineData(0, DefaultDbUser.Email, DefaultDbUser.Password)]
    [InlineData(0, "avalid@email.com.xz", "a valid password")]
    [InlineData(1, "", DefaultDbUser.Password)]
    [InlineData(1, null, DefaultDbUser.Password)]
    [InlineData(1, "invalid", DefaultDbUser.Password)]
    [InlineData(1, "invalid@", DefaultDbUser.Password)]
    [InlineData(1, "invalid@email", DefaultDbUser.Password)]
    [InlineData(1, "invalid@email.", DefaultDbUser.Password)]
    [InlineData(1, DefaultDbUser.Email, "")]
    [InlineData(1, DefaultDbUser.Email, null)]
    [InlineData(2, "invalid@email.", "")]
    internal void AuthenticationRequestTests(int failsExpected, string email, string password)
    {
        var request = new AuthenticationRequest(email, password);
        var context = new ValidationContext(request);

        Assert.Equal(failsExpected, request.Validate(context).Count());
    }
}