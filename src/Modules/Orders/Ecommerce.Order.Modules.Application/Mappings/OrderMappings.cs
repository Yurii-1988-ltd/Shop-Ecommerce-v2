using Ecommerce.Modules.Users.Contracts.Dto;
using Ecommerce.Order.Modules.Application.Features.Responses;

internal static class OrderMappings
{
    public static OrderResponse ToResponse(
        this Order order,
        UserDto? user)
    {
        var totalResult = order.GetTotalAmount();
        var totalAmount = totalResult.IsSuccess ? totalResult.Value.Amount : 0m;
        var currency = totalResult.IsSuccess ? totalResult.Value.Currency : order.Currency;

        // Определяем имя покупателя: Пользователь -> Адрес доставки -> Guest
        var customerName = user is not null
            ? $"{user.FirstName} {user.LastName}".Trim()
            : !string.IsNullOrWhiteSpace(order.ShippingAddress?.FirstName)
                ? $"{order.ShippingAddress.FirstName} {order.ShippingAddress.LastName}".Trim()
                : "Guest";

        return new OrderResponse(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            customerName,
            order.CustomerEmail, // 👈 Полезно добавить Email в детальный ответ
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
                    x.Id,
                    x.ProductId,
                    x.ProductName,
                    x.SKU,
                    x.UnitPrice.Amount,
                    x.UnitPrice.Currency,
                    x.Quantity,
                    x.TotalPrice.Amount))
                .ToList(),
            order.TotalQuantity,
            totalAmount,
            currency,
            order.CreatedAtUtc,
            order.PaidAtUtc,
            order.ShippedAtUtc,
            order.CancelledAtUtc,
            order.CancellationReason);
    }

    internal static OrderListResponse ToListResponse(
        this Order order,
        UserDto? user)
    {
        var totalResult = order.GetTotalAmount();
        var totalAmount = totalResult.IsSuccess ? totalResult.Value.Amount : 0m;
        var currency = totalResult.IsSuccess ? totalResult.Value.Currency : order.Currency;

        var customerName = user is not null
            ? $"{user.FirstName} {user.LastName}".Trim()
            : !string.IsNullOrWhiteSpace(order.ShippingAddress?.FirstName)
                ? $"{order.ShippingAddress.FirstName} {order.ShippingAddress.LastName}".Trim()
                : "Guest";

        return new OrderListResponse(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            customerName,
            order.Status.ToString(),
            order.TotalQuantity,
            totalAmount,
            currency,
            order.CreatedAtUtc);
    }

    internal static OrderListResponse ToMyOrderListResponse(
        this Order order)
    {
        var totalResult = order.GetTotalAmount();
        var totalAmount = totalResult.IsSuccess ? totalResult.Value.Amount : 0m;
        var currency = totalResult.IsSuccess ? totalResult.Value.Currency : order.Currency;

        return new OrderListResponse(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            CustomerName: null,
            order.Status.ToString(),
            order.TotalQuantity,
            totalAmount,
            currency,
            order.CreatedAtUtc);
    }
}