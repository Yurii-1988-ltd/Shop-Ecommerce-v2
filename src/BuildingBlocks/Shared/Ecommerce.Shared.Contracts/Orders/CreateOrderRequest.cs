using Ecommerce.Shared.Contracts.Orders;

public sealed record CreateOrderRequest(
    Guid? CustomerId,
    string CustomerEmail,
    OrderAddressDto ShippingAddress,
    IReadOnlyCollection<CreateOrderItemDto> Items,
    string Currency,
    Guid? GuestId = null);