using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Domain;
using Ecommerce.Notification.Modules.Application.Abstractions;
using Ecommerce.Shared.Contracts.IntegrationEvent;

namespace Ecommerce.Notification.Modules.Application.Services;

public sealed class OrderNotificationService(
    INotificationRepository repository,
    INotificationUnitOfWork unitOfWork,
    IEmailSender emailSender)
    : IOrderNotificationService
{
    public async Task<Result> SendOrderCreatedAsync(
        OrderCreatedIntegrationEvent message,
        CancellationToken cancellationToken = default)
    {
        var subject =
            $"Your order #{message.OrderId} has been accepted!";

        var body = $"""
            <h2>Thank you for your order!</h2>
            <p>Your order <strong>#{message.OrderId}</strong> has been accepted.</p>
            <p>Total: <strong>{message.TotalAmount}</strong></p>
            """;

        var notificationResult = Domain.Entities.Notification.Create(
            message.CustomerEmail,
            subject,
            body);

        if (notificationResult.IsFailure)
            return notificationResult.Error;

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
            notification.MarkAsFailed(sendResult.Error);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            return sendResult.Error;
        }

        notification.MarkAsSent();

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}