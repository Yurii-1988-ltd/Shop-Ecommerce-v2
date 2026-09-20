

using Ecommerce.Domain.Domain;
using Ecommerce.Notification.Modules.Domain.Enums;
using Ecommerce.Notification.Modules.Domain.Errors;

namespace Ecommerce.Notification.Modules.Domain.Entities;

    public sealed class Notification : Entity
    {
        public string Recipient { get; private set; } = default!;
        public string Subject { get; private set; } = default!;
        public string Body { get; private set; } = default!;
    public NotificationStatus Status { get;private set; }
    public DateTime CreatedAtUtc { get;private set; }
    public DateTime? SentAtUtc { get;private set; }

    public string? ErrorCode { get;private set; }
    public string? ErrorDescription { get;private set; }

    private Notification() { }
    private Notification(string recipient,string subject,string body)
    {
        Id = Guid.NewGuid();
        Recipient = recipient;
        Subject = subject;
        Body = body;
        Status = NotificationStatus.Pending;
        CreatedAtUtc = DateTime.UtcNow;
    }
    #region static factory methods
    public static  Result<Notification>Create(string recipient,string subject,string body)
    {
        if (string.IsNullOrWhiteSpace(recipient))
            return NotificationErrors.EmptyRecipient;
        if (string.IsNullOrWhiteSpace(subject))
            return NotificationErrors.EmptySubject;
        var notification  =  new Notification(recipient,subject,body);
        return notification;
            
    }
    // Change status to "Sent"
    public Result MarkAsSent()
    {
        if (Status == NotificationStatus.Sent)
            return NotificationErrors.AlreadySent;
        Status = NotificationStatus.Sent;
        SentAtUtc = DateTime.UtcNow;
        ErrorCode = null; 
        ErrorDescription = null;
        return  Result.Success();
    }
    public Result MarkAsfailed(Error error)
    {
        Status = NotificationStatus.Failed;
        ErrorCode = error.Code;
        ErrorDescription = error.Description;
        return Result.Success();
    }
    #endregion



}

