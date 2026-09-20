

using System.Net;
using System.Net.Http.Json;

namespace Ecommerce.Cart.Modules.Infrastructure.Integrations.Inventory;

internal sealed class InventoryAvailability(HttpClient httpClient) : IInventoryAvailability
{
    public async Task<int?> GetAvailableQuantityAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        // 1. Используем GetAsync с явной передачей CancellationToken
        using var response = await httpClient.GetAsync(
            $"api/inventories/product/{productId}",
            cancellationToken);

        // 2. Обработка 404 (товар не найден или остаток не заведен)
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        // 3. Десериализация
        var result = await response.Content
            .ReadFromJsonAsync<InventoryAvailabilityResponse>(cancellationToken);

        return result?.AvailableQuantity;
    }
}

