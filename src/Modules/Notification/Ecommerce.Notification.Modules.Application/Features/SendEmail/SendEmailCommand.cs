
using Ecommerce.Application.CQRS;

namespace Ecommerce.Notification.Modules.Application.Features.SendEmail
{
    public record SendEmailCommand(string Recipient, string Subject, string Body,
        CancellationToken cancelationToken = default) : ICommand;
   
}
