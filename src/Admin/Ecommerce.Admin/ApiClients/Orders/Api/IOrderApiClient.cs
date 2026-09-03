using Ecommerce.Admin.ApiClients.Orders.Contracts;
using Ecommerce.Admin.ApiClients.Orders.Responses;
using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Orders.Api;

public interface IOrderApiClient
{
    Task<PagedResult<OrderListResponse>?> GetAllAsync(int page,
        int pageSize, 
        string? search = null,
        CancellationToken cancellation = default);
    Task AddOrderItemAsync(Guid orderId, AddOrderItemRequest request,
                            CancellationToken cancellationToken = default);
    Task CancelAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task ChangeOrderItemQuantityAsync(
                            Guid orderId,
                            Guid orderItemId,
                            ChangeOrderItemQuantityRequest request,
                            CancellationToken cancellationToken = default);
    Task ChangeStatusAsync(Guid orderId, ChangeStatusRequest statusRequest,
        CancellationToken cancellationToken = default);
    Task<OrderResponse?> GetAsync(Guid orderId,

                                        CancellationToken cancellationToken = default);
    Task SubmitAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task UpdateShippingAddressAsync(Guid orderId, UpdateShippingAddressRequest request, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(
CreateOrderRequest request,
CancellationToken cancellationToken = default);
    Task ForceChangeStatusAsync(Guid orderId, ForceChangeStatusRequest request, CancellationToken cancellationToken = default);
    Task RemoveOrderItemAsync(Guid orderId, Guid orderItemId, CancellationToken cancellationToken = default);

}