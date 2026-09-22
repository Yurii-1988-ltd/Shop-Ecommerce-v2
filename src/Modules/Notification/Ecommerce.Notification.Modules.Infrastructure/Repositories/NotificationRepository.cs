
using Ecommerce.Notification.Modules.Infrastructure.Database;

namespace Ecommerce.Notification.Modules.Infrastructure.Repositories;

internal sealed class NotificationRepository(NotificationsContext context) : INotificationRepository
{
    public async Task AddAsync(Domain.Entities.Notification notification, CancellationToken cancellationToken = default)
    {
        await context.Notifications.AddAsync(notification, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
    }

    public Task UpdateAsync(
    Domain.Entities.Notification notification,
    CancellationToken cancellationToken = default)
    {
        context.Notifications.Update(notification);

        return Task.CompletedTask;
    }
}
