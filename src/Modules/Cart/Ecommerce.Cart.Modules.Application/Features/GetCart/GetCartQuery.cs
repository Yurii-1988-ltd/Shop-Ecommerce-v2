using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Application.Features.Responses;

namespace Ecommerce.Cart.Modules.Application.Features.GetCart;

public record GetCartQuery(Guid CustomerId) : IQuery<CartResponse>;
