using FluentEmail.Core;

namespace Ecommerce.Notifications.Modules.Infrastructure.Adapters;


public sealed class FluentEmailSender(IFluentEmail fluentEmail,
    ILogger<FluentEmailSender> logger) : IEmailSender
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
                .Body(body,isHtml: true)
                .SendAsync(cancellationToken);
            if(!response.Successful)
            {
                var errors = string.Join(", ",response.ErrorMessages);
                logger.LogError("Error send email {Recipient}: {Errors}", recipient, errors);
                return Error.Problem("Notification.EmailError", errors);
            }
            logger.LogInformation("✉️ [FluentEmail] Email send successfully {Recipient}", recipient);
            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при эмуляции отправки Email на {Recipient}", recipient);
            return Error.Problem("Notification.Exception", ex.Message);
        }
    }
}