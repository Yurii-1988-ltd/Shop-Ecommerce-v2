namespace Ecommerce.Storefront.ApiClients.Cart.Models;

public sealed record CartItemResponse(
    Guid ProductId,
    string Name,
    decimal Price,
    string Currency,
    int Quantity,
    decimal TotalPrice);