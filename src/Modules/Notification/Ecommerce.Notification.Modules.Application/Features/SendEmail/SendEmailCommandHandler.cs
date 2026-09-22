

using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Notification.Modules.Application.Abstractions;

namespace Ecommerce.Notification.Modules.Application.Features.SendEmail;

internal sealed class SendEmailCommandHandler(IEmailSender emailSender,
    INotificationRepository repository) : ICommandHandler<SendEmailCommand>
{
    public async Task<Result> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
       var notificationResult = Domain.Entities.Notification.Create(request.Recipient, request.Subject, request.Body);
        if (notificationResult.IsFailure)
        {
            return notificationResult.Error;
        }
        var notification = notificationResult.Value;
        var sendResult = await emailSender.SendAsync(notification.Recipient, notification.Subject, notification.Body,cancellationToken);
        if (sendResult.IsSuccess)
        {
            notification.MarkAsSent();
        }
        else
        {
            notification.MarkAsFailed(sendResult.Error);
        }
        await repository.AddAsync(notification, cancellationToken);
        return sendResult;
    }
}
