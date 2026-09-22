using Ecommerce.Notification.Modules.Application.Services;
using Ecommerce.Shared.Contracts.IntegrationEvent;
using MassTransit;

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
            "Processing OrderCreated event for order {OrderId}",
            message.OrderId);

        var result = await notificationService.SendOrderCreatedAsync(
            message,
            context.CancellationToken);

        if (result.IsFailure)
        {
            logger.LogError(
                "Failed to process notification for order {OrderId}: {Error}",
                message.OrderId,
                result.Error.Description);

            throw new InvalidOperationException(
                result.Error.Description);
        }
    }
}