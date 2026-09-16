

using Ecommerce.Application.CQRS;

namespace Ecommerce.Cart.Modules.Application.Features.AddCartItem;

public sealed record AddCartItemCommand(
    Guid? CustomerId,
    Guid? GuestId,
    Guid ProductId,
    string Name,
    decimal Price,
    string Currency,
    int Quantity) : ICommand;