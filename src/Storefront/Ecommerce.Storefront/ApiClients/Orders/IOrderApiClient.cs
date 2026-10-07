using Ecommerce.Application.Pagination;
using Ecommerce.Shared.Contracts.Orders;


namespace Ecommerce.Storefront.ApiClients.Orders;

public interface IOrderApiClient
{
    Task<CreateOrderResponse> CreateAsync(CreateOrderRequest request,
        CancellationToken cancellationToken = default);
    Task<PagedResult<OrderListResponse>> GetMyOrdersAsync(
         int page = 1,
         int pageSize = 20,
         CancellationToken cancellationToken = default);
}