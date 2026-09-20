

namespace Ecommerce.Notifications.Modules.Infrastructure.Adapters;

internal sealed class EmailSender(ILogger<EmailSender> logger) : IEmailSender
{
    public async Task<Result> SendAsync(
        string recipient,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("--------------------------------------------------");
            logger.LogInformation("📨 [DEV EMAIL SENDER]");
            logger.LogInformation("To: {Recipient}", recipient);
            logger.LogInformation("Subject: {Subject}", subject);
            logger.LogInformation("Body: {Body}", body);
            logger.LogInformation("--------------------------------------------------");

            await Task.Delay(100, cancellationToken); // Симуляция сетевой задержки

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при эмуляции отправки Email на {Recipient}", recipient);
            return Error.Problem("Notification.Exception", ex.Message);
        }
    }
}