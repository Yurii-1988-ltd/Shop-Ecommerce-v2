

namespace Ecommerce.Cart.Modules.Application.Features.GetCart;

internal sealed class GetCartQueryHandler(ICartRepository repository) : IQueryHandler<GetCartQuery,CartResponse>
{
    public async Task<Result<CartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        if(request.CustomerId.HasValue && request.GuestId.HasValue)
        {
            return CartErrors.OwnerConflict;
        }
        if(request.CustomerId.HasValue)
        {
            var cart = await repository.GetByCustomerIdAsync(request.CustomerId.Value, cancellationToken);
            if (cart is null)
                return CartErrors.NotFound(request.CustomerId.Value);
            return cart.ToResponse();


        }
        if(request.GuestId.HasValue)
        {
            var cart = await repository.GetByGuestIdAsync(request.GuestId.Value, cancellationToken);
            if (cart is null)
                return CartErrors.NotFound(request.GuestId.Value);
            return cart.ToResponse();
        }
        return CartErrors.OwnerRequired;




    }
}