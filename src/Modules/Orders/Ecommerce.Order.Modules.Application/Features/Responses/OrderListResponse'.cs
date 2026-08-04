using Ecommerce.Order.Modules.Domain.Enums;

namespace Ecommerce.Order.Modules.Application.Features.Responses;

public sealed record OrderListResponse(
    Guid Id,
    string OrderName,
    Guid CustomerId,
    string CustomerName,
    OrderStatus Status,
    int TotalQuantity,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAtUtc);
