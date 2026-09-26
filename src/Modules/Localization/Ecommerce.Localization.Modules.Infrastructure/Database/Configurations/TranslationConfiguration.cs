

using Ecommerce.Localization.Modules.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Localization.Modules.Infrastructure.Database.Configurations;

internal sealed class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.ToTable("translations");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedNever();
        builder.Property(x => x.Key)
           .HasMaxLength(200)
           .IsRequired();

        builder.Property(x => x.CultureCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired();

        builder.Property(x => x.Module)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasIndex(
                x => new
                {
                    x.Key,
                    x.CultureCode
                })
            .IsUnique();

    }
}
