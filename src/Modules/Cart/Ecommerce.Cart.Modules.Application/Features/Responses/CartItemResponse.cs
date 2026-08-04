using Ecommerce.Cart.Modules.Application.Features.Responses;
using Ecommerce.Cart.Modules.Domain.Entities;

public sealed record CartItemResponse(
    Guid ProductId,
    string Name,
    decimal Price,
    string Currency,
    int Quantity,
    decimal TotalPrice);