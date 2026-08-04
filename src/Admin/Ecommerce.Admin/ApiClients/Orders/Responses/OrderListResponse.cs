using Ecommerce.Admin.ApiClients.Orders.Enums;

namespace Ecommerce.Admin.ApiClients.Orders.Responses
{
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
}
