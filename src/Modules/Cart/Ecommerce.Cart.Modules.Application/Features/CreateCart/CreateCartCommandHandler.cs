namespace Ecommerce.Cart.Modules.Application.Features.CreateCart;

internal sealed class CreateCartCommandHandler(
    ICartRepository cartRepository)
    : ICommandHandler<CreateCartCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateCartCommand request,
        CancellationToken cancellationToken)
    {
        if (request.CustomerId.HasValue && request.GuestId.HasValue)
            return CartErrors.OwnerConflict;

        if (!request.CustomerId.HasValue && !request.GuestId.HasValue)
            return CartErrors.OwnerRequired;

        if (request.CustomerId.HasValue)
        {
            var existing = await cartRepository.GetByCustomerIdAsync(
                request.CustomerId.Value,
                cancellationToken);

            if (existing is not null)
                return CartErrors.AlreadyExists(request.CustomerId.Value);

            var cartResult = Domain.Entities.Cart.CreateForCustomer(
                request.CustomerId.Value);

            if (cartResult.IsFailure)
                return cartResult.Error;

            await cartRepository.InsertAsync(
                cartResult.Value,
                cancellationToken);

            return cartResult.Value.Id;
        }

        var guestId = request.GuestId!.Value;

        var existingGuest = await cartRepository.GetByGuestIdAsync(
            guestId,
            cancellationToken);

        if (existingGuest is not null)
            return existingGuest.Id;

        var guestCartResult = Domain.Entities.Cart.CreateForGuest(
            guestId);

        if (guestCartResult.IsFailure)
            return guestCartResult.Error;

        await cartRepository.InsertAsync(
            guestCartResult.Value,
            cancellationToken);

        return guestCartResult.Value.Id;
    }
}