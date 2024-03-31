using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtwiseDatabase.Entities.Config;

/// <summary>
/// Configures relationships for <see cref="AdditionRequestEntity"/>.
/// </summary>
internal sealed class AdditionRequestEntityTypeConfiguration : IEntityTypeConfiguration<AdditionRequestEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AdditionRequestEntity> builder)
        => builder.HasKey(x => x.Id);
}