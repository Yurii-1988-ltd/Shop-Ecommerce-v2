namespace Ecommerce.Storefront.ApiClients.Carts.Models;

public sealed record AddCartItemRequest(
    Guid ProductId,
    string Name,
    string Sku,
    decimal Price,
    string Currency,
    int Quantity
);
