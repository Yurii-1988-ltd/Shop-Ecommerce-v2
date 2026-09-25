using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Notification.Modules.Infrastructure.Database.Configurations;

internal sealed class NotificationConfiguration
    : IEntityTypeConfiguration<Domain.Entities.Notification>
{
    public void Configure(
        EntityTypeBuilder<Domain.Entities.Notification> builder)
    {
        builder.ToTable("notifications");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .ValueGeneratedNever();

        builder.Property(n => n.Recipient)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(n => n.Subject)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(n => n.Body)
            .IsRequired();

        builder.Property(n => n.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(n => n.CreatedAtUtc)
            .IsRequired();

        builder.Property(n => n.SentAtUtc)
            .IsRequired(false);

        builder.Property(n => n.ErrorCode)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(n => n.ErrorDescription)
            .HasMaxLength(1000)
            .IsRequired(false);
    }
}