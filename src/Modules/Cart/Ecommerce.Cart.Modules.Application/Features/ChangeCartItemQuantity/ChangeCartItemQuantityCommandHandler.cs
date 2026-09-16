
namespace Ecommerce.Cart.Modules.Application.Features.ChangeCartItemQuantity;

internal sealed class ChangeCartItemQuantityCommandHandler(ICartRepository repository): ICommandHandler<ChangeCartItemQuantityCommand>
{
 
        public async Task<Result> Handle(
            ChangeCartItemQuantityCommand request,
            CancellationToken cancellationToken)
        {
            if(request.CustomerId.HasValue && request.GuestId.HasValue)
                return CartErrors.OwnerConflict;
        if (!request.CustomerId.HasValue && !request.GuestId.HasValue)
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
            return CartErrors.NotFound(request.CustomerId??request.GuestId??Guid.Empty);
        var result = cart.ChangeQuantity(request.ProductId, request.Quantity);
        
        if (result.IsFailure)
                return result;

            await repository.UpdateAsync(cart, cancellationToken);

            return Result.Success();
        }
    
}