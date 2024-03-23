using ArtwiseDatabase.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArtwiseDatabase.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionExt
{
    /// <summary>
    /// Adds <see cref="ArtwiseDbContext"/> as a scoped service to this IoC.
    /// </summary>
    /// <param name="serviceCollection">This service collection.</param>
    /// <param name="connectionString">The connection string to the database.</param>
    /// <param name="migrate"><see langword="true"/> if a migration should be performed, <see langword="false"/> otherwise.</param>
    /// <returns>This service collection.</returns>
    public static IServiceCollection AddArtwiseDb(this IServiceCollection serviceCollection, string? connectionString = default, bool migrate = true)
    {
        // Perform the migration
        // If the database doesn't exist, create it
        if (migrate)
        {
            Directory.CreateDirectory(ArtwiseDbStatics.DatabaseDirectory);
            using var dbContext = new ArtwiseDbContext(ArtwiseDbStatics.GetDefaultDbOptions<ArtwiseDbContext>(null, connectionString).Options);
            dbContext.Database.Migrate();
        }

        // Add the database context to the IoC container
        serviceCollection.AddDbContext<ArtwiseDbContext>(options => ArtwiseDbStatics.GetDefaultDbOptions(options, connectionString));

        return serviceCollection;
    }
}