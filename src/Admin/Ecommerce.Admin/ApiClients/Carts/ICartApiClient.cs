
using Ecommerce.Admin.ApiClients.Carts.Contracts;
using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Carts;

public interface ICartApiClient
{

    Task<PagedResult<CartListResponse>?> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    // Получить корзину пользователя
    Task<CartResponse?> GetAsync(
        Guid customerId,
        CancellationToken cancellationToken = default);

    // Создать корзину (если оставляешь CreateCart)
    Task<Guid> CreateAsync(
        CreateCartRequest request,
        CancellationToken cancellationToken = default);

    // Добавить товар
    Task AddItemAsync(
        Guid customerId,
        AddCartItemRequest request,
        CancellationToken cancellationToken = default);

    // Изменить количество
    Task ChangeQuantityAsync(
        Guid customerId,
        Guid productId,
        ChangeCartItemQuantityRequest request,
        CancellationToken cancellationToken = default);


    Task RemoveItemAsync(
        Guid customerId,
        Guid productId,
        CancellationToken cancellationToken = default);

  
    Task ClearAsync(
        Guid customerId,
        CancellationToken cancellationToken = default);

    Task RemoveFromCartAsync(
        Guid customerId,
        CancellationToken cancellationToken = default
    );
    Task ApplyCouponAsync(Guid customerId,
                          ApplyCouponRequest request,
                          CancellationToken cancellationToken = default
                            );
    Task RemoveCouponAsync(Guid customerId, CancellationToken cancellationToken = default);
}
