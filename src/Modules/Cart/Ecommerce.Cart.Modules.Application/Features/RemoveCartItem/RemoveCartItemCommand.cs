using Ecommerce.Application.CQRS;

namespace Ecommerce.Cart.Modules.Application.Features.RemoveCartItem;

public record RemoveCartItemCommand(Guid CustomerId, Guid ProductId): ICommand;