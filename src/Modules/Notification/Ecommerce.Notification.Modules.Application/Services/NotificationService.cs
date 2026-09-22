using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Domain;
using Ecommerce.Notification.Modules.Application.Abstractions;
using Ecommerce.Notification.Modules.Application.Services;
using Ecommerce.Notification.Modules.Domain.Entities;
using Ecommerce.Shared.Contracts.IntegrationEvent;

public sealed class NotificationService(
    INotificationRepository repository,
    IUnitOfWork unitOfWork,
    IEmailSender emailSender)
    : INotificationService
{
    public async Task<Result> SendOrderCreatedAsync(
        OrderCreatedIntegrationEvent message,
        CancellationToken cancellationToken)
    {
        var subject =
            $"Your order #{message.OrderId} has been accepted!";

        var body = $"""
            <h2>Thank you for your order!</h2>
            <p>Order: {message.OrderId}</p>
            <p>Total: {message.TotalAmount} USD</p>
            """;

        var notificationResult = Notification.Create(
            message.CustomerEmail,
            subject,
            body);

        if (notificationResult.IsFailure)
            return notificationResult.Error;

        var notification = notificationResult.Value;

        await repository.AddAsync(
            notification,
            cancellationToken);

        var sendResult = await emailSender.SendAsync(
            notification.Recipient,
            notification.Subject,
            notification.Body,
            cancellationToken);

        if (sendResult.IsFailure)
        {
            notification.MarkAsFailed(sendResult.Error);

            await repository.UpdateAsync(
                notification,
                cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return sendResult.Error;
        }

        notification.MarkAsSent();

        await repository.UpdateAsync(
            notification,
            cancellationToken);

        return Result.Success();
    }
}