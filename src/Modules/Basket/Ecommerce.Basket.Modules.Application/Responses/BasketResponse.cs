


namespace Ecommerce.Basket.Modules.Application.Responses;

public sealed record BasketResponse(Guid Id,
    Guid CustomerId,
    List<BasketItemResponse>Items,
    decimal TotalPrice,
    string Currency);

