using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Domain;
using Ecommerce.Notification.Modules.Application.Abstractions;
using Ecommerce.Notification.Modules.Application.Models;
using Ecommerce.Shared.Contracts.IntegrationEvent;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Notification.Modules.Application.Services;

public sealed class NotificationService(
    INotificationRepository repository,
    INotificationUnitOfWork unitOfWork,
    IEmailSender emailSender,
    IEmailTemplateRenderer templateRenderer,
    ILogger<NotificationService> logger)
    : INotificationService
{
    public async Task<Result> SendOrderCreatedAsync(
        OrderCreatedIntegrationEvent message,
        CancellationToken cancellationToken = default)
    {
        var subject =
            $"Your order #{message.OrderNumber} has been accepted!";

        var emailItems = message.Items
            .Select(item => new OrderCreatedEmailItemModel(
                ProductName: item.ProductName,
                Sku: item.Sku,
                UnitPrice: item.UnitPrice,
                Quantity: item.Quantity,
                LineTotal: item.UnitPrice * item.Quantity))
            .ToList();

        var emailModel = new OrderCreatedEmailModel(
            OrderNumber: message.OrderNumber,
            TotalAmount: message.TotalAmount,
            Currency: message.Currency,
            Items: emailItems);

        var templateResult =
            await templateRenderer.RenderAsync(
                "OrderCreated",
                emailModel,
                cancellationToken);

        if (templateResult.IsFailure)
        {
            logger.LogError(
                "Failed to render email template for order {OrderNumber}: {Error}",
                message.OrderNumber,
                templateResult.Error.Description);

            return templateResult.Error;
        }

        var notificationResult =
            Domain.Entities.Notification.Create(
                message.CustomerEmail,
                subject,
                templateResult.Value);

        if (notificationResult.IsFailure)
        {
            logger.LogError(
                "Failed to create notification for order {OrderNumber}: {Error}",
                message.OrderNumber,
                notificationResult.Error.Description);

            return notificationResult.Error;
        }

        var notification = notificationResult.Value;

        await repository.AddAsync(
            notification,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        var sendResult = await emailSender.SendAsync(
            notification.Recipient,
            notification.Subject,
            notification.Body,
            cancellationToken);

        if (sendResult.IsFailure)
        {
            logger.LogError(
                "Failed to send notification for order {OrderNumber} to {Recipient}: {Error}",
                message.OrderNumber,
                notification.Recipient,
                sendResult.Error.Description);

            notification.MarkAsFailed(sendResult.Error);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            return sendResult.Error;
        }

        notification.MarkAsSent();

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        logger.LogInformation(
            "Notification for order {OrderNumber} sent successfully to {Recipient}",
            message.OrderNumber,
            notification.Recipient);

        return Result.Success();
    }
}