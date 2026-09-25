namespace Ecommerce.Notification.Modules.Application.Models;

public sealed record OrderCreatedEmailItemModel(
    string ProductName,
    string Sku,
    decimal UnitPrice,
    int Quantity,
    decimal LineTotal);
