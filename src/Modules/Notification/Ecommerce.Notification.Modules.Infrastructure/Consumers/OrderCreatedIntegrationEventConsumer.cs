using Ecommerce.Notification.Modules.Application.Services;
using Ecommerce.Shared.Contracts.IntegrationEvent;
using MassTransit;

namespace Ecommerce.Notification.Modules.Infrastructure.Consumers;

public sealed class OrderCreatedIntegrationEventConsumer(
    INotificationService notificationService,
    ILogger<OrderCreatedIntegrationEventConsumer> logger)
    : IConsumer<OrderCreatedIntegrationEvent>
{
    public async Task Consume(
        ConsumeContext<OrderCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        logger.LogInformation(
            "Processing order {OrderId}, CustomerId {CustomerId}, CustomerEmail '{CustomerEmail}'",
            message.OrderId,
            message.CustomerId,
            message.CustomerEmail);

        var result = await notificationService.SendOrderCreatedAsync(
            message,
            context.CancellationToken);

        if (result.IsFailure)
        {
            logger.LogError(
                "Failed to send notification for order {OrderId}: {Error}",
                message.OrderId,
                result.Error.Description);

            throw new InvalidOperationException(
                result.Error.Description);
        }

        logger.LogInformation(
            "Notification for order {OrderId} processed successfully",
            message.OrderId);
    }
    
    
}