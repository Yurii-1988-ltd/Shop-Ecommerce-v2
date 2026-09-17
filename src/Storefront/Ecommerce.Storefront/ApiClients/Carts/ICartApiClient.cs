using Ecommerce.Storefront.ApiClients.Carts.Models;

namespace Ecommerce.Storefront.ApiClients.Carts;

public interface ICartApiClient
{
    Task AddItemAsync(
     AddCartItemRequest request,
     CancellationToken cancellationToken = default);
    Task ChangeQuantityAsync(
    Guid productId,
    ChangeCartItemQuantityRequest request,
    CancellationToken cancellationToken = default);
    Task RemoveItemAsync(
    Guid productId,
    CancellationToken cancellationToken = default);
  Task<CartResponse?> GetAsync(
    CancellationToken cancellationToken = default);
    Task <Guid>CreateAsync(CancellationToken cancellationToken = default);
      
}
