namespace Ecommerce.Storefront.ApiClients.Carts.Services
{
    public interface ICartMergeService
    {
        Task MergeGuestCartAsync(CancellationToken cancellationToken = default);
    }
}
