
namespace Ecommerce.Cart.Modules.Application.Features.RemoveCartItem;

internal sealed class RemoveCartItemCommandHandler(ICartRepository repository): ICommandHandler<RemoveCartItemCommand>
{
    public async Task<Result> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        if (request.CustomerId.HasValue && request.GuestId.HasValue)
            return CartErrors.OwnerConflict;
        if (!request.CustomerId.HasValue&& !request.GuestId.HasValue)
            return CartErrors.OwnerRequired;
        Domain.Entities.Cart? cart;
        if (request.CustomerId.HasValue)
        {
            cart = await repository.GetByCustomerIdAsync(request.CustomerId.Value, cancellationToken);
        }
        else
        {
            cart = await repository.GetByGuestIdAsync(request.GuestId!.Value, cancellationToken);
        }
        
        if (cart is null)
            return CartItemErrors.NotFound(request.CustomerId??request.GuestId??Guid.Empty);
        var result = cart.RemoveItem(request.ProductId);
        if (result.IsFailure)
            return result;
        await repository.UpdateAsync(cart,cancellationToken);
        return Result.Success();


    }
}