

using Ecommerce.Domain.Domain;

namespace Ecommerce.Notification.Modules.Domain.Errors;

public static class NotificationErrors
{
    public static  Error InvalidRecipient =>new (
        "Notification.InvalidRecipient", "Invalid or empty email recipient specified", ErrorType.Validation);
    public static Error SendFailed => new(
        "Notification.SendFailed", "Failed to send the email via the mail provider.", ErrorType.Problem);
    public static Error ProviderError(string details) =>
        new("Notification.ProviderError", $"The email provider returned an error{details}", ErrorType.Problem);
    public static Error AlreadySent => new("Already.Sent", "Email already sent", ErrorType.Validation);
    public static  Error EmptyRecipient=> new(
        "Notification.EmptyRecipient", "Invalid  empty  recipient specified", ErrorType.Validation);
    public static Error EmptySubject=> new(
        "Notification.EmptySubject", "Invalid  empty subject specified", ErrorType.Validation);

}
