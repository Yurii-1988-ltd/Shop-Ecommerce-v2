

namespace Ecommerce.Notification.Modules.Application.Abstractions;

public interface INotificationUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
   
}
