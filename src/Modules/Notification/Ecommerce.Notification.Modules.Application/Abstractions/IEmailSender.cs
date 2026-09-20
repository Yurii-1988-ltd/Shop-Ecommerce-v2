

using Ecommerce.Domain.Domain;

namespace Ecommerce.Notification.Modules.Application.Abstractions;

public interface IEmailSender
{
    Task<Result>SendAsync(string recipient, string subject, string body, CancellationToken cancellation = default);
}
