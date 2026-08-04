
namespace Ecommerce.Identity.Modules.Infrastructure.Configurations;

internal sealed class PasswordResetTokenConfiguration
    : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable("password_reset_tokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.ExpiresOnUtc)
            .IsRequired();

        builder.Property(x => x.UsedOnUtc);

        builder.Property(x => x.RevokedOnUtc);

        builder.HasIndex(x => x.Token)
            .IsUnique();

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.ExpiresOnUtc
        });
    }
}