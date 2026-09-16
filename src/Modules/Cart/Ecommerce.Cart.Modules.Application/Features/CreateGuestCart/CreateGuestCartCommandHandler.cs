

namespace Ecommerce.Cart.Modules.Application.Features.CreateGuestCart;

internal sealed class CreateGuestCartCommandHandler(ICartRepository cartRepository) : ICommandHandler<CreateGuestCartCommand, Guid>
{

    public async Task<Result<Guid>> Handle(
        CreateGuestCartCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await cartRepository.GetByGuestIdAsync(
            request.GuestId,
            cancellationToken);
        if (existing != null)
        {
            return existing.Id;
        }
        var cartResult = Domain.Entities.Cart.CreateForGuest(request.GuestId);
        if(cartResult.IsFailure)
            return cartResult.Error;
        await cartRepository.InsertAsync(
            cartResult.Value,
            cancellationToken);

        return cartResult.Value.Id;

    }

}
