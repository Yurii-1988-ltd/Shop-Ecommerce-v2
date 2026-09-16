using Ecommerce.Storefront.ApiClients.Carts.Models;
using Ecommerce.Storefront.ApiClients.Carts.Services;

namespace Ecommerce.Storefront.ApiClients.Cart;

internal sealed class CartApiClient(HttpClient httpClient,
    ICurrentUser currentUser,IGuestCartService guestCartService) : ICartApiClient
{
    private const string CartUrl = "/carts";

    public async Task AddItemAsync(
  
        AddCartItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{CartUrl}/items?{GetOwnerQuery()}",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeQuantityAsync(
        Guid productId,
        ChangeCartItemQuantityRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(
                 $"{CartUrl}/items/{productId}?{GetOwnerQuery()}",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveItemAsync(
  
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
             $"{CartUrl}/items/{productId}?{GetOwnerQuery()}",
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<CartResponse?> GetAsync(
 
        CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<CartResponse>(
            $"{CartUrl}?{GetOwnerQuery()}",
            cancellationToken);
    }
    private string GetOwnerQuery()
    {
        if (currentUser.IsAuthenticated)
            return $"customerId={currentUser.UserId}";

        var guestId = guestCartService.GetOrCreateGuestId();

        return $"guestId={guestId}";
    }

    public async Task<Guid> CreateCartAsync(CancellationToken cancellationToken = default)
    {
        CreateCartRequest request;
        if (currentUser.IsAuthenticated)
        {
            request = new CreateCartRequest(currentUser.UserId, null);

        }
        else
        {
            var guestId = guestCartService.GetOrCreateGuestId();
            request = new CreateCartRequest(null, guestId);
        }
        var response = await httpClient.PostAsJsonAsync(
            $"{CartUrl}",
            request,
            cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken);
    }
}