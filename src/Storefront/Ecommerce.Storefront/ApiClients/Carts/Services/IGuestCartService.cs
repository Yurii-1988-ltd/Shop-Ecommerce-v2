namespace Ecommerce.Storefront.ApiClients.Carts.Services;

public interface IGuestCartService
{
    Guid GetGuestId(CancellationToken cancellationToken = default);
    Guid? TryGetGuestId();
 
}