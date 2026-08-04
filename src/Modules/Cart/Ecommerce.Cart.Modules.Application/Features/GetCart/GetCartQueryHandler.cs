using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Application.Features.Responses;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Application.Features.GetCart;

internal sealed class GetCartQueryHandler(ICartRepository repository) : IQueryHandler<GetCartQuery,CartResponse>
{
    public async Task<Result<CartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetByCustomerIdAsync(request.CustomerId);
        if (cart is null)
            return CartErrors.NotFound(request.CustomerId);
        return cart.ToResponse();


    }
}