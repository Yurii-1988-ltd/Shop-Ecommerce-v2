using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Constants;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Order.Modules.Domain.ValueObjects;
using Ecommerce.Shared.Contracts.IntegrationEvent;
using MassTransit;

public sealed class CreateOrderCommandHandler(
    IOrderRepository repository,
    IEntityNumberGenerator orderNumberGenerator,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerEmail))
        {
            return Error.Failure(
                "Order.CustomerEmailRequired",
                "Customer email is required.");
        }

        var orderNumber =
            await orderNumberGenerator.GenerateAsync(
                NumberPrefixes.Order,
                cancellationToken);

        var shippingAddress = new OrderAddress(
            request.ShippingAddress.FirstName,
            request.ShippingAddress.LastName,
            request.ShippingAddress.Country,
            request.ShippingAddress.City,
            request.ShippingAddress.Street,
            request.ShippingAddress.ZipCode);

        var orderResult = Order.Create(
            request.CustomerId,
            orderNumber,
            shippingAddress,
            request.Currency);

        if (orderResult.IsFailure)
            return orderResult.Error;

        var order = orderResult.Value;

        foreach (var item in request.Items)
        {
            var moneyResult = Money.Create(
                item.UnitPrice,
                request.Currency);

            if (moneyResult.IsFailure)
                return moneyResult.Error;

            var addItemResult = order.AddItem(
                item.ProductId,
                item.ProductName,
                item.Sku,
                moneyResult.Value,
                item.Quantity);

            if (addItemResult.IsFailure)
                return addItemResult.Error;
        }

        // Рассчитываем итоговую сумму заказа
        var totalResult = order.GetTotalAmount();

        if (totalResult.IsFailure)
            return totalResult.Error;

        await repository.InsertAsync(
            order,
            cancellationToken);

        await publishEndpoint.Publish(
            new OrderCreatedIntegrationEvent(
                OrderId: order.Id,
                CustomerId: order.CustomerId,
                CustomerEmail: request.CustomerEmail,
                TotalAmount: totalResult.Value.Amount,
                Currency: order.Currency,
                CreatedAtUtc: DateTime.UtcNow),
            cancellationToken);

        return order.Id;
    }
}