

using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Application.RemoveCoupon;

internal sealed class RemoveCouponCommandHandler(ICartRepository repository) : ICommandHandler<RemoveCouponCommand>
{
    public async Task<Result> Handle(RemoveCouponCommand request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

        if (cart is null)
            return CartErrors.NotFound(request.CustomerId);

      
        var removeResult = cart.RemoveCoupon();
        if (removeResult.IsFailure)
            return removeResult;

   
        await repository.UpdateAsync(cart, cancellationToken);

        return Result.Success();
    }
}
