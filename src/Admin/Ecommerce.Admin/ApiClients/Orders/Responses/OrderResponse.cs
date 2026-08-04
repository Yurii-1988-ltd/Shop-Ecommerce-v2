using Ecommerce.Admin.ApiClients.Orders.Enums;

namespace Ecommerce.Admin.ApiClients.Orders.Responses;

public sealed record OrderResponse(
  Guid Id,
  string OrderNumber,
  Guid CustomerId,
  string CustomerName,
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
