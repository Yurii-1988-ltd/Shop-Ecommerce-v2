

namespace Ecommerce.Identity.Modules.Infrastructure.Configurations;

internal sealed class IdentityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasIndex(rt => rt.Token).IsUnique();
    }
   
}
