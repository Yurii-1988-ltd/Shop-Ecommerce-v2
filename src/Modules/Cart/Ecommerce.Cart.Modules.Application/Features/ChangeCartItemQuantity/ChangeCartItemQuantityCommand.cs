using Ecommerce.Application.CQRS;

namespace Ecommerce.Cart.Modules.Application.Features.ChangeCartItemQuantity;

public sealed record ChangeCartItemQuantityCommand(
    Guid CustomerId,
    Guid ProductId,
    int Quantity)
    : ICommand;

