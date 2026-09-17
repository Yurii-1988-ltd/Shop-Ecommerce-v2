using Ecommerce.Storefront.ApiClients.Carts.Models;
using Ecommerce.Storefront.ApiClients.Carts.Services;
using System.Net;

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
        var ownerQuery = GetOwnerQuery();


        var response = await httpClient.GetAsync(
            $"{CartUrl}?{ownerQuery}",
            cancellationToken);


        if (response.StatusCode == HttpStatusCode.NotFound)
        {
      

            var cartId = await CreateAsync(cancellationToken);

       

            ownerQuery = GetOwnerQuery();

       

            response = await httpClient.GetAsync(
                $"{CartUrl}?{ownerQuery}",
                cancellationToken);

        

            response.EnsureSuccessStatusCode();
        }
        else
        {
            response.EnsureSuccessStatusCode();
        }

        return await response.Content.ReadFromJsonAsync<CartResponse>(
            cancellationToken);
    }
    private string GetOwnerQuery()
    {
        if (currentUser.IsAuthenticated)
            return $"customerId={currentUser.UserId}";

        var guestId = guestCartService.GetGuestId();

        return $"guestId={guestId}";
    }

    public async Task<Guid> CreateAsync(CancellationToken cancellationToken = default)
    {
        CreateCartRequest request;
        if (currentUser.IsAuthenticated)
        {
            request = new CreateCartRequest(currentUser.UserId, null);

        }
        else
        {
            var guestId = guestCartService.GetGuestId();
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