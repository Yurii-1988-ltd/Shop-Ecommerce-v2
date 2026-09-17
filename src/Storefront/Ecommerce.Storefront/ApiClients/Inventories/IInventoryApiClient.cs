using Ecommerce.Storefront.ApiClients.Inventories.Models;

namespace Ecommerce.Storefront.ApiClients.Inventories;

public interface IInventoryApiClient
{
    Task<InventoryAvailabilityResponse> GetAvailabilityAsync(
        Guid productId,
        CancellationToken cancellationToken = default);
}
