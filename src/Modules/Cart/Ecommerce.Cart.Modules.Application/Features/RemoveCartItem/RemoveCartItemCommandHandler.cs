using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Application.Features.RemoveCartItem;

internal sealed class RemoveCartItemCommandHandler(ICartRepository repository): ICommandHandler<RemoveCartItemCommand>
{
    public async Task<Result> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (cart is null)
            return CartItemErrors.NotFound(request.CustomerId);
        var result = cart.RemoveItem(request.ProductId);
        if (result.IsFailure)
            return result;
        await repository.UpdateAsync(cart,cancellationToken);
        return Result.Success();


    }
}