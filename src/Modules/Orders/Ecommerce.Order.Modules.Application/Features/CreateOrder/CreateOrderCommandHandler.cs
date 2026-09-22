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
        var orderNumber = await orderNumberGenerator.GenerateAsync(
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

            var result = order.AddItem(
                item.ProductId,
                item.ProductName,
                item.Sku,
                moneyResult.Value,
                item.Quantity);

            if (result.IsFailure)
                return result.Error;
        }

        var count = order.Items.Count;

        // Сохраняем заказ в БД
        await repository.InsertAsync(order, cancellationToken);

        // 2. Публикуем событие в RabbitMQ для Notifications и других модулей
        await publishEndpoint.Publish(new OrderCreatedIntegrationEvent(
            OrderId: order.Id,
            CustomerId: order.CustomerId,
            CustomerEmail: request.CustomerEmail, 
            TotalAmount: order.TotalQuantity,       
            CreatedAtUtc: DateTime.UtcNow
        ), cancellationToken);

        return order.Id;
    }
}