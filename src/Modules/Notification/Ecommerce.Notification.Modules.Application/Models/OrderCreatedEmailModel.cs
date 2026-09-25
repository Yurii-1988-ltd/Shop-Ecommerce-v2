namespace Ecommerce.Notification.Modules.Application.Models;

public sealed record OrderCreatedEmailModel(
    Guid OrderId,
    decimal TotalAmount,
    string Currency);