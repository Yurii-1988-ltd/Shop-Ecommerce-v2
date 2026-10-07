using Ecommerce.Order.Modules.Domain.Enums;

namespace Ecommerce.Order.Modules.Application.Features.Responses;

public sealed record OrderResponse(
    Guid Id,
    string OrderNumber,
    Guid? CustomerId,
    string CustomerName,
    string CustomerEmail, 
    OrderStatus Status,
    AddressResponse ShippingAddress,
    IReadOnlyCollection<OrderItemResponse> Items,
    int TotalQuantity,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAtUtc,
    DateTime? PaidAtUtc,
    DateTime? ShippedAtUtc,
    DateTime? CancelledAtUtc,
    string? CancellationReason);