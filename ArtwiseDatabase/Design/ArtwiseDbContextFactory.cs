using ArtwiseDatabase.Common;
using Microsoft.EntityFrameworkCore.Design;

namespace ArtwiseDatabase.Design;

/// <summary>
/// This class is only used at design time, when EF Core is asked to perform a migration.
/// </summary>
internal sealed class ArtwiseDbContextFactory : IDesignTimeDbContextFactory<ArtwiseDbContext>
{
    public ArtwiseDbContext CreateDbContext(string[] args)
        => new(ArtwiseDbStatics.GetDefaultDbOptions<ArtwiseDbContext>().Options);
}