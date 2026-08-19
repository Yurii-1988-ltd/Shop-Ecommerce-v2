namespace Ecommerce.Basket.Modules.Application.Responses;

public sealed record BasketItemResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    string Currency,
    int Quantity);