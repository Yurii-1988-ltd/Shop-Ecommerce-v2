using Ecommerce.Shared.Contracts.Orders;
using Ecommerce.Storefront.ApiClients.Carts.Models;
using Ecommerce.Storefront.ApiClients.Carts.Services;
using Ecommerce.Storefront.ApiClients.Identity.Contracts;
using System.Net;

namespace Ecommerce.Storefront.ApiClients.Cart;

internal sealed class CartApiClient(HttpClient httpClient,
    ICurrentUser currentUser, IGuestCartService guestCartService) : ICartApiClient
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

        // Если корзина не найдена, создаем её и повторяем попытку добавления товара
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest && error.Contains("Cart.NotFound"))
            {
                // Создаем корзину
                await CreateAsync(cancellationToken);

                // Повторяем попытку добавления
                response = await httpClient.PostAsJsonAsync(
                    $"{CartUrl}/items?{GetOwnerQuery()}",
                    request,
                    cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                // Вычитываем новое сообщение об ошибке, если повторный запрос снова упал
                error = await response.Content.ReadAsStringAsync(cancellationToken);
            }

            throw new HttpRequestException(
                $"Cart API returned {(int)response.StatusCode}: {error}");
        }
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
  
public async Task ClearAsync(
    CancellationToken cancellationToken = default)
    {
        if (!currentUser.IsAuthenticated)
            throw new InvalidOperationException(
                "Clearing a guest cart is not supported by this endpoint.");

        var response = await httpClient.DeleteAsync(
            $"{CartUrl}/{currentUser.UserId}/items",
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task MergeAsync(Guid guestId, Guid customerId, CancellationToken cancellationToken = default)
    {
        var request = new  MergeGuestCartRequest(guestId, customerId);
        var response = await httpClient.PostAsJsonAsync(
            $"{CartUrl}/merge",
            request,
            cancellationToken);
        response.EnsureSuccessStatusCode();

    }

    public async Task ClearGuestCartAsync(Guid guestId, CancellationToken cancellationToken = default)
    {
        if (guestId == Guid.Empty)
        {
            return;
        }

   
        var requestUri = $"{CartUrl.TrimEnd('/')}/guests/{guestId}";

        var response = await httpClient.DeleteAsync(requestUri, cancellationToken);

    
        response.EnsureSuccessStatusCode();
    }

    public async Task ClearGuestCartAsync(CancellationToken cancellationToken = default)
    {
        var guestId = guestCartService.GetGuestId(cancellationToken);

        if (guestId == Guid.Empty)
        {
            return;
        }

        // В Storefront API маршрут для гостей: /carts/guests/{guestId}
        var requestUri = $"api/carts/guests/{guestId}";
        var response = await httpClient.DeleteAsync(requestUri, cancellationToken);

        // Игнорируем 404, если корзина уже пустая/удалена, чтобы не ломать поток заказа
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return;
        }

        response.EnsureSuccessStatusCode();
    }


}