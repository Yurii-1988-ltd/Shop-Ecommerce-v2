using Ecommerce.Storefront.ApiClients.Inventories.Models;

namespace Ecommerce.Storefront.ApiClients.Inventories;

internal sealed class InventoryApiClient(HttpClient httpClient) : IInventoryApiClient
{
    public const string InventoryUrl = "/inventories";
    public async Task<InventoryAvailabilityResponse> GetAvailabilityAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<InventoryAvailabilityResponse>($"{InventoryUrl}/product/{productId}", cancellationToken);
    }
}
