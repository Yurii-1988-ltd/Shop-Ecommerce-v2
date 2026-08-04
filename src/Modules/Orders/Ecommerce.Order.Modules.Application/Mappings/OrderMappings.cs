using Ecommerce.Modules.Users.Contracts.Dto;
using Ecommerce.Order.Modules.Application.Features.Responses;

internal static class OrderMappings
{
    public static OrderResponse ToResponse(
        this Ecommerce.Order.Modules.Domain.Entities.Order order,
        UserDto? user)
    {
        var total = order.GetTotalAmount().Value;

        var customerName = user is null
            ? "Unknown"
            : $"{user.FirstName} {user.LastName}";

        return new OrderResponse(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            customerName,
            order.Status,
            new AddressResponse(
                order.ShippingAddress.FirstName,
                order.ShippingAddress.LastName,
                order.ShippingAddress.Country,
                order.ShippingAddress.City,
                order.ShippingAddress.Street,
                order.ShippingAddress.ZipCode),
            order.Items
                .Select(x => new OrderItemResponse(
                    x.ProductId,
                    x.ProductName,
                    x.SKU,
                    x.UnitPrice.Amount,
                    x.UnitPrice.Currency,
                    x.Quantity,
                    x.TotalPrice.Amount))
                .ToList(),
            order.TotalQuantity,
            total.Amount,
            total.Currency,
            order.CreatedAtUtc,
            order.PaidAtUtc,
            order.ShippedAtUtc,
            order.CancelledAtUtc,
            order.CancellationReason);
    }

    internal static OrderListResponse ToListResponse(
        this Ecommerce.Order.Modules.Domain.Entities.Order order,
        UserDto? user)
    {
        var total = order.GetTotalAmount().Value;

        var customerName = user is null
            ? "Unknown"
            : $"{user.FirstName} {user.LastName}";

        return new OrderListResponse(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            customerName,
            order.Status,
            order.TotalQuantity,
            total.Amount,
            total.Currency,
            order.CreatedAtUtc);
    }
}