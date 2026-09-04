using Ecommerce.Storefront.ApiClients.Cart.Models;
using Ecommerce.Storefront.ApiClients.Carts;
using Ecommerce.Storefront.ApiClients.Carts.Models;
using System.Net.Http.Json;

namespace Ecommerce.Storefront.ApiClients.Cart;

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

    public async Task<CartResponse?> GetAsync(
        Guid customerId,
        CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<CartResponse>(
            $"{CartUrl}/{customerId}",
            cancellationToken);
    }
}