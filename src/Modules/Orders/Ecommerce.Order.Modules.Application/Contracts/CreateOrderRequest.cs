

namespace Ecommerce.Order.Modules.Application.Contracts;

public sealed record CreateOrderRequest(
    Guid CustomerId,
    string CustomerEmail,
    OrderAddressDto ShippingAddress,
    List<CreateOrderItemDto> Items,
    string Currency);
