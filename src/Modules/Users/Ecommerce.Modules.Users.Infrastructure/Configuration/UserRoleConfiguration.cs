

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Modules.Users.Infrastructure.Configuration;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x=>x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new
        {
            x.UserId,
            x.RoleId,

        })
            .IsUnique();
    }
}
