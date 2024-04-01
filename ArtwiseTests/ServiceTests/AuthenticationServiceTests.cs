using ArtwiseAPI.Features.Authentication.Services;
using ArtwiseDatabase.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ArtwiseTests.ServiceTests;

public sealed class AuthenticationServiceTests : BaseApiServiceTest
{
    private readonly AuthenticationService _service;

    public AuthenticationServiceTests(ServicesFixture fixture) : base(fixture)
        => _service = base.Scope.ServiceProvider.GetRequiredService<AuthenticationService>();

    [Fact]
    internal async Task LoginTestAsync()
    {
        // Use the service
        var response = await _service.LoginAsync(new(DefaultDbUser.Email, DefaultDbUser.Password));

        // Test the result
        Assert.Equal(DefaultDbUser.Id, response.Id);
        Assert.NotEmpty(response.SessionToken);
    }

    [Fact]
    internal void LoginEmailNotFoundTest()
        => Assert.ThrowsAsync<InvalidOperationException>(() => _service.LoginAsync(new("doesnt@exist.com", DefaultDbUser.Password)));


    [Fact]
    internal void LoginWrongPasswordTest()
        => Assert.ThrowsAsync<InvalidOperationException>(() => _service.LoginAsync(new(DefaultDbUser.Email, DefaultDbUser.Password + "1")));

    [Fact]
    internal void GenerateSessionTokenUserTest()
    {
        var token = _service.GenerateSessionToken(DefaultDbUser.Instance, DateTime.UtcNow.AddDays(1));
        Assert.NotEmpty(token);
    }

    [Theory]
    [InlineData("", 1)]
    [InlineData(null, 1)]
    [InlineData(DefaultDbUser.Email, 0)]
    [InlineData(DefaultDbUser.Email, -1)]
    internal void GenerateSessionTokenUserFailTest(string email, double days)
    {
        var user = DefaultDbUser.Instance with { Email = email };
        Assert.ThrowsAny<ArgumentException>(() => _service.GenerateSessionToken(user, DateTime.UtcNow.AddDays(days)));
    }

    [Theory]
    [InlineData(DefaultDbUser.Email, 7)]
    [InlineData(DefaultDbUser.Email, 1)]
    [InlineData(DefaultDbUser.Email, 0.5)]
    [InlineData(DefaultDbUser.Email, 0.1)]
    internal void GenerateSessionTokenTest(string email, double days)
    {
        var token = _service.GenerateSessionToken(email, UserType.Administrator, DateTime.UtcNow.AddDays(days));
        Assert.NotEmpty(token);
    }
}