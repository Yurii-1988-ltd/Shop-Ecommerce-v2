namespace Ecommerce.Notification.Modules.Infrastructure.Configuration;

public sealed class SmtpOptions
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 1025;
    public bool UseSsl { get; set; }

    public string? Username { get; set; }
    public string? Password { get; set; }

    public string FromEmail { get; set; } =
        "noreply@ecommerce.com";

    public string FromName { get; set; } =
        "Ecommerce Store";
}