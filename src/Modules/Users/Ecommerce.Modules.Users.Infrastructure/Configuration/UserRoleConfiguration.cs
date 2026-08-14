

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Modules.Users.Infrastructure.Configuration;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasIndex(x => new
        {
            x.UserId,
            x.RoleId,

        })
            .IsUnique();
    }
}
