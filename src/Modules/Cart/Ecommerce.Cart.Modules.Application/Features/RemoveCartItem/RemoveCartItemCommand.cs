

namespace Ecommerce.Cart.Modules.Application.Features.RemoveCartItem;

public record RemoveCartItemCommand(Guid? CustomerId, Guid? GuestId, Guid ProductId): ICommand;