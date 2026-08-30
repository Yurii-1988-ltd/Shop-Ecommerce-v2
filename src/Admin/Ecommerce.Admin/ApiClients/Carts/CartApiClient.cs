using Ecommerce.Admin.ApiClients.Carts.Contracts;
using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Carts;

internal sealed class CartApiClient(HttpClient httpClient) : ICartApiClient
{
    private const string CartUrl = "/carts";
    public async Task AddItemAsync(
        Guid customerId,
        AddCartItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{CartUrl}/{customerId}/items",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }
    public async Task ChangeQuantityAsync(
 Guid customerId,
 Guid productId,
 ChangeCartItemQuantityRequest request,
 CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(
            $"{CartUrl}/{customerId}/items/{productId}",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task ClearAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"{CartUrl}/{customerId}/items", cancellationToken);
        response.EnsureSuccessStatusCode();

    }

    public async Task RemoveFromCartAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"{CartUrl}/{customerId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
    }

    public async Task<Guid> CreateAsync(CreateCartRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"{CartUrl}",
                                                            request,
                                                            cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken);
      
    }

    public async Task<PagedResult<CartListResponse>?> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<PagedResult<CartListResponse>>(
            $"{CartUrl}?page={page}&pageSize={pageSize}",
            cancellationToken);
    }

    public async Task<CartResponse?> GetAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<CartResponse>($"{CartUrl}/{customerId}", cancellationToken);
        
    }

    public async Task RemoveItemAsync(
     Guid customerId,
     Guid productId,
     CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
            $"{CartUrl}/{customerId}/items/{productId}",
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task ApplyCouponAsync(Guid customerId, ApplyCouponRequest request, CancellationToken cancellationToken = default)
    {
       var response = await httpClient.PostAsJsonAsync($"{CartUrl}/{customerId}/coupon",
                                        request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveCouponAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
                                $"{CartUrl}/{customerId}/coupon",cancellationToken); 
        response.EnsureSuccessStatusCode();
       
    }
}
