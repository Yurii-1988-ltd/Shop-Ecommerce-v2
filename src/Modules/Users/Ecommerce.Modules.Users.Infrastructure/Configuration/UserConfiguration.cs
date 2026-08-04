

using Ecommerce.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Modules.Users.Infrastructure.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users","dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired();
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
        builder.Property(x=>x.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x=>x.LastName)
            .IsRequired()
            .HasMaxLength(100);
    }
}
