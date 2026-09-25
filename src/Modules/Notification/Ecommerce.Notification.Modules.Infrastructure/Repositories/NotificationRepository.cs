

namespace Ecommerce.Notification.Modules.Infrastructure.Repositories;

internal sealed class NotificationRepository(
    NotificationsContext context) : INotificationRepository
{
    public async Task AddAsync(
        Domain.Entities.Notification notification,
        CancellationToken cancellationToken = default)
    {
        await context.Notifications.AddAsync(
            notification,
            cancellationToken);
    }

    //public Task UpdateAsync(
    //    Domain.Entities.Notification notification,
    //    CancellationToken cancellationToken = default)
    //{
    //    context.Notifications.Update(notification);

    //    return Task.CompletedTask;
    //}
}