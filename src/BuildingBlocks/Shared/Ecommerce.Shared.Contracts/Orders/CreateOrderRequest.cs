

namespace Ecommerce.Shared.Contracts.Orders;

public sealed record CreateOrderRequest(
    Guid CustomerId,
    string CustomerEmail,
    OrderAddressDto ShippingAddress,
    List<CreateOrderItemDto> Items,
    string Currency);
