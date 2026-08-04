using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Application.Features.ChangeCartItemQuantity;

internal sealed class ChangeCartItemQuantityCommandHandler(ICartRepository repository): ICommandHandler<ChangeCartItemQuantityCommand>
{
 
        public async Task<Result> Handle(
            ChangeCartItemQuantityCommand request,
            CancellationToken cancellationToken)
        {
            var cart = await repository.GetByCustomerIdAsync(
                request.CustomerId,
                cancellationToken);

            if (cart is null)
                return CartErrors.NotFound(request.CustomerId);

            var result = cart.ChangeQuantity(
                request.ProductId,
                request.Quantity);

            if (result.IsFailure)
                return result;

            await repository.UpdateAsync(cart, cancellationToken);

            return Result.Success();
        }
    
}