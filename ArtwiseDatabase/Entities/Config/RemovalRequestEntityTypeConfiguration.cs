using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtwiseDatabase.Entities.Config;

/// <summary>
/// Configures relationships for <see cref="RemovalRequestEntity"/>.
/// </summary>
internal sealed class RemovalRequestEntityTypeConfiguration : IEntityTypeConfiguration<RemovalRequestEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RemovalRequestEntity> builder)
        => builder.HasKey(x => new { x.ArtId, x.Email });
}