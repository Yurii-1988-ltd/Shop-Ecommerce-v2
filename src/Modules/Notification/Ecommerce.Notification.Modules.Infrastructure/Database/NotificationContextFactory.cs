


namespace Ecommerce.Notification.Modules.Infrastructure.Database;

internal sealed class NotificationContextFactory : IDesignTimeDbContextFactory<NotificationsContext>
{
    public NotificationsContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
           new DbContextOptionsBuilder<NotificationsContext>();

        optionsBuilder.UseNpgsql(
     "Host=localhost;Port=5432;Database=notifications;Username=postgres;Password=postgres");

        return new NotificationsContext(optionsBuilder.Options);
    }
}
