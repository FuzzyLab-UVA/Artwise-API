using Microsoft.EntityFrameworkCore;

namespace ArtwiseDatabase.Common;

/// <summary>
/// Helper class for initialization of an <see cref="ArtwiseDbContext"/>.
/// </summary>
internal static class ArtwiseDbStatics
{
    /// <summary>
    /// The directory where the database is located at.
    /// </summary>
    internal static string DatabaseDirectory { get; } = Path.Join(AppContext.BaseDirectory, "Data");

    /// <summary>
    /// Defines where the database is located at.
    /// </summary>
    internal static string DatabasePath { get; } = Path.Join(DatabaseDirectory, "Artwise.db");

    /// <summary>
    /// The default database connection string.
    /// </summary>
    /// <remarks>Points to the current directory of the application. Has the format "Data Source=Data/Artwise.db"</remarks>
    internal static string DefaultConnectionString { get; } = "Data Source=" + DatabasePath;

    /// <summary>
    /// Gets a database options builder, adds the default settings, and returns it.
    /// </summary>
    /// <param name="options">The database options to have the default settings applied to.</param>
    /// <param name="connectionString">The connection string of the database.</param>
    /// <typeparam name="T">The type of the <see cref="DbContext"/>.</typeparam>
    /// <returns>A database options with the default settings applied.</returns>
    internal static DbContextOptionsBuilder<T> GetDefaultDbOptions<T>(DbContextOptionsBuilder<T>? options = default, string? connectionString = default) where T : DbContext
    {
        options ??= new();

        return options.UseSnakeCaseNamingConvention()                       // Set column names to snake_case format
            .UseSqlite(connectionString ?? DefaultConnectionString)         // Database connection string
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);    // Disable EF Core entity tracking - see https://docs.microsoft.com/en-us/ef/core/change-tracking/
    }

    /// <summary>
    /// Gets a database options builder, adds the default settings, and returns it.
    /// </summary>
    /// <param name="options">The database options to have the default settings applied to.</param>
    /// <param name="connectionString">The connection string of the database.</param>
    /// <returns>A database options with the default settings applied.</returns>
    internal static DbContextOptionsBuilder GetDefaultDbOptions(DbContextOptionsBuilder? options = default, string? connectionString = default)
    {
        options ??= new();

        return options.UseSnakeCaseNamingConvention()                       // Set column names to snake_case format
            .UseSqlite(connectionString ?? DefaultConnectionString)         // Database connection string
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);    // Disable EF Core entity tracking - see https://docs.microsoft.com/en-us/ef/core/change-tracking/
    }
}