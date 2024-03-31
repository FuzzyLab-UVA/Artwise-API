using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtwiseDatabase.Entities.Config;

/// <summary>
/// Configures relationships for <see cref="ArtEntity"/>.
/// </summary>
internal sealed class ArtEntityTypeConfiguration : IEntityTypeConfiguration<ArtEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ArtEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.RemovalRequests)
            .WithOne(x => x.Art)
            .HasForeignKey(x => x.ArtId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tags)
            .WithOne(x => x.Art)
            .HasForeignKey(x => x.ArtId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}