 
namespace Ecommerce.Shared.Contracts.Orders;

public sealed record CreateOrderResponse(
    Guid OrderId,
    string OrderNumber);

