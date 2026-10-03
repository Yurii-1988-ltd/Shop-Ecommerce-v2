

namespace Ecommerce.Cart.Modules.Application.Features.RemoveCart;

public record RemoveCartCommand(Guid CustomerId): ICommand;