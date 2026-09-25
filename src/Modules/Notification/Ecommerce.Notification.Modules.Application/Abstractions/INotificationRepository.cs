


namespace Ecommerce.Notification.Modules.Application.Abstractions;

public interface INotificationRepository
{
    Task AddAsync(Domain.Entities.Notification notification, CancellationToken cancellationToken = default);
   // Task UpdateAsync(Domain.Entities.Notification notification, CancellationToken cancellationToken = default);
}
