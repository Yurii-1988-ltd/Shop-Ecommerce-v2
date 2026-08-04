

namespace Ecommerce.Admin.ApiClients.Orders.Api;

internal sealed class OrderApiClient(HttpClient httpClient) : IOrderApiClient
{
    private const string OrderUrl = "/orders";

    public async Task AddOrderItemAsync(Guid orderId, AddOrderItemRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
                $"{OrderUrl}/{orderId}/items",
                    request, cancellationToken);
        response.EnsureSuccessStatusCode();
     
    }

    public async Task CancelAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsync($"{OrderUrl}/{orderId}/cancel",
            content: null,
            cancellationToken);
        response.EnsureSuccessStatusCode();
        
    }

    public async Task ChangeOrderItemQuantityAsync(
     Guid orderId,
     Guid orderItemId,
     ChangeOrderItemQuantityRequest request,
     CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync(
            $"{OrderUrl}/{orderId}/items/{orderItemId}/quantity",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeStatusAsync(Guid orderId, ChangeStatusRequest statusRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"{OrderUrl}/{orderId}/status",
            statusRequest,
            cancellationToken);
        response.EnsureSuccessStatusCode();
       
    }

    public async Task<Guid> CreateAsync(
       CreateOrderRequest request,
       CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            OrderUrl,
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken); ;
    }

    public async Task ForceChangeStatusAsync(Guid orderId, ForceChangeStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"{OrderUrl}/{orderId}/force-change-status", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<PagedResult<OrderListResponse>?> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<PagedResult<OrderListResponse>>(
            $"{OrderUrl}?page={page}&pageSize={pageSize}", cancellationToken);
  
    }

    public async Task<OrderResponse?> GetAsync(
     Guid orderId,
     CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(
            $"{OrderUrl}/{orderId}",
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<OrderResponse>(cancellationToken)
            ?? throw new InvalidOperationException("Order response was null.");
    }

    public async Task SubmitAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"{OrderUrl}/{orderId}/submit", 
            content: null,
            cancellationToken);
        response.EnsureSuccessStatusCode();
      
    }

    public async Task UpdateShippingAddressAsync(Guid orderId, UpdateShippingAddressRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"{OrderUrl}/{orderId}/shipping-address", request, cancellationToken);
        response.EnsureSuccessStatusCode();
       
    }
}
