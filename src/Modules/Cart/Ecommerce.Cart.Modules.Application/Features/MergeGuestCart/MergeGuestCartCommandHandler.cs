
namespace Ecommerce.Cart.Modules.Application.Features.MergeGuestCart;

internal sealed class MergeGuestCartCommandHandler(ICartRepository repository) : ICommandHandler<MergeGuestCartCommand>
{
    public async Task<Result> Handle(MergeGuestCartCommand request, CancellationToken cancellationToken)
    {
        var guestCart = await repository.GetByGuestIdAsync(request.GuestId, cancellationToken);
        if (guestCart is null)
            return Result.Success();
        var customerCart = await repository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        var isNewCart = customerCart is null;
        if (isNewCart)
        {
            var createResult = Domain.Entities.Cart.CreateForCustomer(request.CustomerId);
            if (createResult.IsFailure)
                return createResult.Error;
            customerCart = createResult.Value;
        }
        var mergeResult = customerCart!.Merge(guestCart);
        if (mergeResult.IsFailure)
            return mergeResult;
        if (isNewCart)
        {
            await repository.InsertAsync(customerCart, cancellationToken);

        }
        else
        {
            await repository.ReplaceAsync(customerCart, cancellationToken);
        }
        await repository.DeleteAsync(guestCart.Id, cancellationToken);
        return Result.Success();
    } 
}
