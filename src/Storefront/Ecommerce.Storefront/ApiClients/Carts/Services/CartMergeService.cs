using Ecommerce.Shared.Contracts.Orders;

namespace Ecommerce.Storefront.ApiClients.Carts.Services;

public sealed class CartMergeService(ICartApiClient cartApiClient,
    IGuestCartService guestCartService,
    ICurrentUser currentUser)
    : ICartMergeService
{
    public async Task MergeGuestCartAsync(CancellationToken cancellationToken = default)
    {
        if(!currentUser.IsAuthenticated)
            return;
        var guestId = guestCartService.TryGetGuestId();
        if (guestId is null)
            return;

        await cartApiClient.MergeAsync(guestId.Value, currentUser.UserId, cancellationToken);

    }
}
