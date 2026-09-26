

using Ecommerce.Localization.Modules.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Localization.Modules.Infrastructure.Database.Configurations;

internal sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("languages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();
        builder.Property(x => x.CultureCode)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(x=>x.Name) .IsRequired()
            .HasMaxLength (20);
        builder.Property(x => x.NativeName).IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.IsDefault)
            .IsRequired();
        builder.Property(x=>x.IsEnabled) 
            .IsRequired();
        builder.HasIndex(x => x.CultureCode)
            .IsUnique() ;



    }
}
