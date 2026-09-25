namespace Ecommerce.Notification.Modules.Application.Models;

public sealed record OrderCreatedEmailModel(
    string OrderNumber,
    decimal TotalAmount,
    string Currency,
    IReadOnlyCollection<OrderCreatedEmailItemModel> Items);