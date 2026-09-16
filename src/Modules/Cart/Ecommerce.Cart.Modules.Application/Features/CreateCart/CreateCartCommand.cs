

namespace Ecommerce.Cart.Modules.Application.Features.CreateCart;

public sealed record CreateCartCommand(Guid? CustomerId, Guid? GuestId) : ICommand<Guid>;


