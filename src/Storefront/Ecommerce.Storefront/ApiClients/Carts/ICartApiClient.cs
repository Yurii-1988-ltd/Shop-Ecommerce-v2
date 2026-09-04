using Ecommerce.Storefront.ApiClients.Carts.Models;

namespace Ecommerce.Storefront.ApiClients.Carts;

public interface ICartApiClient
{
    Task AddItemAsync(
     Guid customerId,
     AddCartItemRequest request,
     CancellationToken cancellationToken = default);
    Task ChangeQuantityAsync(
    Guid customerId,
    Guid productId,
    ChangeCartItemQuantityRequest request,
    CancellationToken cancellationToken = default);
    Task RemoveItemAsync(
    Guid customerId,
    Guid productId,
    CancellationToken cancellationToken = default);
    Task<CartResponse?> GetAsync(
      Guid customerId,
      CancellationToken cancellationToken = default);
}
