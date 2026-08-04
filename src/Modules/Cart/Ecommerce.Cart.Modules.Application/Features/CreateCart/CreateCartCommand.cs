

using Ecommerce.Application.CQRS;

namespace Ecommerce.Cart.Modules.Application.Features.CreateCart;

public sealed record CreateCartCommand(Guid CustomerId) : ICommand<Guid>;


