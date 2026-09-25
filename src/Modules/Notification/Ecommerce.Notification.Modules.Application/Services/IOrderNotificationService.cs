

using Ecommerce.Domain.Domain;
using Ecommerce.Shared.Contracts.IntegrationEvent;

namespace Ecommerce.Notification.Modules.Application.Services;

public interface IOrderNotificationService
{
    Task<Result> SendOrderCreatedAsync(
       OrderCreatedIntegrationEvent message,
       CancellationToken cancellationToken = default);
}
