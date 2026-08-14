using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Modules.Users.Infrastructure.Configuration;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Id).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}
