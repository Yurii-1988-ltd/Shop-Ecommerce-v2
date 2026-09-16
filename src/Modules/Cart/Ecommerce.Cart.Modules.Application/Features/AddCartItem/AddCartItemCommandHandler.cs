
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Application.Features.AddCartItem;

internal sealed class AddCartItemCommandHandler(
    ICartRepository repository)
    : ICommandHandler<AddCartItemCommand>
{
    public async Task<Result> Handle(
        AddCartItemCommand request,
        CancellationToken cancellationToken)
    {
        if (request.CustomerId.HasValue && request.GuestId.HasValue)
            return CartErrors.OwnerConflict;

        if (!request.CustomerId.HasValue && !request.GuestId.HasValue)
            return CartErrors.OwnerRequired;

        Domain.Entities.Cart? cart;

        if (request.CustomerId.HasValue)
        {
            cart = await repository.GetByCustomerIdAsync(
                request.CustomerId.Value,
                cancellationToken);
        }
        else
        {
            cart = await repository.GetByGuestIdAsync(
                request.GuestId!.Value,
                cancellationToken);
        }

        if (cart is null)
            return CartErrors.NotFound(
                request.CustomerId ?? request.GuestId ?? Guid.Empty);

        var priceResult = Money.Create(
            request.Price,
            request.Currency);

        if (priceResult.IsFailure)
            return priceResult.Error;

        var result = cart.AddItem(
            request.ProductId,
            request.Name,
            priceResult.Value,
            request.Quantity);

        if (result.IsFailure)
            return result;

        await repository.UpdateAsync(
            cart,
            cancellationToken);

        return Result.Success();
    }
}