using ArtwiseAPI.Common;
using ArtwiseAPI.Extensions;
using ArtwiseDatabase;
using ArtwiseDatabase.Extensions;
using Kotz.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ArtwiseTests.Fixtures;

/// <summary>
/// Fixture for the API's <see cref="IServiceProvider"/>.
/// </summary>
/// <remarks>
/// This class creates one service provider before any of the tests are run and
/// disposes of it when the tests are over. The same service provider is shared
/// among all test classes that inherit from <see cref="BaseApiServiceTest"/>
/// or that have the <see cref="CollectionAttribute"/> with this class' type
/// name applied to it.
/// </remarks>
public sealed class ServicesFixture : IDisposable
{
    private readonly ServiceProvider _serviceProvider;

    /// <summary>
    /// A replica of the API's service provider.
    /// </summary>
    internal IServiceProvider ServiceProvider { get; }

    public ServicesFixture()
    {
        var builder = WebApplication.CreateBuilder();

        _serviceProvider = builder.Services                                 // Add the default services some Artwise services may depend on
            .AddLogging(x => x.ClearProviders())                            // Supress service logging, if any is being used
            .RegisterServices(Assembly.LoadFrom("ArtwiseAPI.dll"))          // Add Artwise services
            .AddArtwiseDb("Data Source=file::memory:?cache=shared;", true)  // Initialize an in-memory SQLite database
            .AddArtwiseAuth(builder.Configuration.GetValue<byte[]>(ApiConstants.JwtAppSetting)!, false)  // Add Artwise authentication and authorization services
            .BuildServiceProvider(true);

        ServiceProvider = _serviceProvider;

        // Add one default user that can
        // be used on unit tests.
        AddDefaultUser(ServiceProvider);
    }

    public void Dispose()
        => _serviceProvider.Dispose();

    /// <summary>
    /// Adds a <see cref="DefaultDbUser.Instance"/> user to the test database
    /// before the tests are run.
    /// </summary>
    /// <param name="services">The IoC container.</param>
    private static void AddDefaultUser(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ArtwiseDbContext>();

        db.Users.Add(DefaultDbUser.Instance);
        db.SaveChanges();
    }
}