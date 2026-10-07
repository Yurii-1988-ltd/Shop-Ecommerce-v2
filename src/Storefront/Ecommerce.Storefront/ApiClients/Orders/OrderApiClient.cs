using Ecommerce.Application.Pagination;
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

        var responseDto =
            await response.Content.ReadFromJsonAsync<CreateOrderResponse>(
                cancellationToken);

        return responseDto!;
    }

    public async Task<PagedResult<OrderListResponse>> GetMyOrdersAsync(
        int page = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(
            $"{OrdersUrl}/my?page={page}&pageSize={pageSize}",
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(
                cancellationToken);

            throw new HttpRequestException(
                $"Orders API returned {(int)response.StatusCode}: {error}");
        }

        var responseDto =
            await response.Content.ReadFromJsonAsync<PagedResult<OrderListResponse>>(
                cancellationToken);

        return responseDto
            ?? throw new InvalidOperationException(
                "Orders API returned an empty response.");
    }
}