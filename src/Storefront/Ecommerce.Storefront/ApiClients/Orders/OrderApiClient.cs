using Ecommerce.Shared.Contracts.Orders;

namespace Ecommerce.Storefront.ApiClients.Orders;

internal sealed class OrderApiClient(HttpClient httpClient) : IOrderApiClient
{
    private const string OrdersUrl = "/orders";

    public async Task<CreateOrderResponse> CreateAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync(
            OrdersUrl,
            request,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(
                cancellationToken);

            throw new HttpRequestException(
                $"Orders API returned {(int)response.StatusCode}: {error}");
        }

        var responseDto = await response.Content.ReadFromJsonAsync<CreateOrderResponse>(
            cancellationToken);

        return responseDto;
    }
}
