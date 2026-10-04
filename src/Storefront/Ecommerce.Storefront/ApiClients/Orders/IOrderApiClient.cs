using Ecommerce.Shared.Contracts.Orders;

namespace Ecommerce.Storefront.ApiClients.Orders;

public interface IOrderApiClient
{
    Task<CreateOrderResponse> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
}
