using ArtwiseDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ArtwiseDatabase;

/// <summary>
/// Represents a database for the Artwise API.
/// </summary>
public sealed class ArtwiseDbContext : DbContext
{
    /// <summary>
    /// The table containing user data.
    /// </summary>
    public DbSet<UserEntity> Users { get; init; }

    /// <summary>
    /// The table containing art data.
    /// </summary>
    public DbSet<ArtEntity> Arts { get; init; }

    /// <summary>
    /// The table containing the art tags.
    /// </summary>
    public DbSet<TagEntity> Tags { get; init; }

    /// <summary>
    /// The table containing the user art addition requests.
    /// </summary>
    public DbSet<AdditionRequestEntity> AdditionRequests { get; init; }

    /// <summary>
    /// The table containing the user art removal requests.
    /// </summary>
    public DbSet<RemovalRequestEntity> RemovalRequests { get; init; }

    /// <summary>
    /// Initializes a <see cref="ArtwiseDbContext"/>.
    /// </summary>
    /// <param name="options">The database options</param>
    public ArtwiseDbContext(DbContextOptions<ArtwiseDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetCollationToProperties<string>(modelBuilder, "NOCASE"); // Set database-wide collation to ignore ASCII case-sensitivity
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Sets collation of all <typeparamref name="T"/> columns to <paramref name="sqlCollation"/>.
    /// </summary>
    /// <typeparam name="T">The type of data to apply the collation to.</typeparam>
    /// <param name="modelBuilder">The model builder.</param>
    /// <param name="sqlCollation">The database collation.</param>
    /// <exception cref="ArgumentNullException">Occurs when <paramref name="modelBuilder"/> or <paramref name="sqlCollation"/> are <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Occurs when <paramref name="sqlCollation"/> is <see langword="null"/> or empty.</exception> 
    private static void SetCollationToProperties<T>(ModelBuilder modelBuilder, string sqlCollation)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlCollation);

        modelBuilder.UseCollation(sqlCollation);

        var stringProperties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(x => x.GetProperties())
            .Where(x => x.ClrType == typeof(T));

        foreach (var property in stringProperties)
            property.SetCollation(sqlCollation);
    }
}