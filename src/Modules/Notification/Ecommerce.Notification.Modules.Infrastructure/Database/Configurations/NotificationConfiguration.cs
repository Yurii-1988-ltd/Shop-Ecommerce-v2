

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Notification.Modules.Infrastructure.Database.Configurations;

internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Domain.Entities.Notification>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Notification> builder)
    {

        builder.ToTable("notifications", "notifications");


        builder.HasKey(n => n.Id);

        // 3. Конфигурация полей
        builder.Property(n => n.Recipient)
            .IsRequired()
            .HasMaxLength(256); // Валидная максимальная длина для Email

        builder.Property(n => n.Subject)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(n => n.Body)
            .IsRequired();

        builder.Property(n => n.Status)
            .IsRequired()
            .HasConversion<int>(); // Храним как целое число (Pending=0, Sent=1, Failed=2)

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