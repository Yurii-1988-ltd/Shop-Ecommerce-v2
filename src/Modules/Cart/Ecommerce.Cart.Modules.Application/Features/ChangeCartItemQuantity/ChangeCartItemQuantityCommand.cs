

namespace Ecommerce.Cart.Modules.Application.Features.ChangeCartItemQuantity;

public sealed record ChangeCartItemQuantityCommand(
    Guid? CustomerId,
    Guid?GuestId,
    Guid ProductId,
    int Quantity)
    : ICommand;

