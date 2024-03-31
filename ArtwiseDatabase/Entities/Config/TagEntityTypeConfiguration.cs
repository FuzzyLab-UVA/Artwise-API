using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtwiseDatabase.Entities.Config;

/// <summary>
/// Configures relationships for <see cref="TagEntity"/>.
/// </summary>
internal sealed class TagEntityTypeConfiguration : IEntityTypeConfiguration<TagEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TagEntity> builder)
        => builder.HasKey(x => new { x.ArtId, x.Tag });
}