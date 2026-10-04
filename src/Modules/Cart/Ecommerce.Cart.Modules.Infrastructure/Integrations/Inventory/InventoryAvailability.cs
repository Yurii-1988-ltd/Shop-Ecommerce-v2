using System.Net;
using System.Net.Http.Json;

namespace Ecommerce.Cart.Modules.Infrastructure.Integrations.Inventory;

internal sealed class InventoryAvailability(HttpClient httpClient)
    : IInventoryAvailability
{
    public async Task<int?> GetAvailableQuantityAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync(
            $"/inventories/product/{productId}",
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<InventoryAvailabilityResponse>(
                cancellationToken);

        return result?.AvailableQuantity;
    }
}