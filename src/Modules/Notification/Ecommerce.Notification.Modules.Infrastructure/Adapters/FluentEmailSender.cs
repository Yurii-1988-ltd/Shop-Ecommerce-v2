
using FluentEmail.Core;

namespace Ecommerce.Notification.Modules.Infrastructure.Adapters;

public sealed class FluentEmailSender(
    IFluentEmail fluentEmail,
    ILogger<FluentEmailSender> logger)
    : IEmailSender
{
    public async Task<Result> SendAsync(
        string recipient,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await fluentEmail
                .To(recipient)
                .Subject(subject)
                .Body(body, isHtml: true)
                .SendAsync(cancellationToken);

            if (!response.Successful)
            {
                var errors = string.Join(
                    " | ",
                    response.ErrorMessages);

                logger.LogError(
                    "Failed to send email to {Recipient}: {Errors}",
                    recipient,
                    errors);

                return Error.Problem(
                    "Notification.EmailError",
                    errors);
            }

            logger.LogInformation(
                "Email sent successfully to {Recipient}",
                recipient);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An error occurred while sending email to {Recipient}",
                recipient);

            return Error.Problem(
                "Notification.Exception",
                ex.Message);
        }
    }
}