

using Ecommerce.Domain.Domain;
using Ecommerce.Shared.Contracts.IntegrationEvent;

namespace Ecommerce.Notification.Modules.Application.Services;

public interface INotificationService
{
    Task<Result> SendOrderCreatedAsync(
       OrderCreatedIntegrationEvent message,
       CancellationToken cancellationToken);
}
