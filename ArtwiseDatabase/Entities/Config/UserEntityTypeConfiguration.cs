using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtwiseDatabase.Entities.Config;

/// <summary>
/// Configures relationships for <see cref="UserEntity"/>.
/// </summary>
internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserEntity> builder)
        => builder.HasKey(x => x.Id);
}