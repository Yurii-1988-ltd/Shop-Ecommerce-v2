using Ecommerce.Application.Abstractions;
using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Constants;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Order.Modules.Domain.Entities;
using Ecommerce.Order.Modules.Domain.Repositories;
using Ecommerce.Order.Modules.Domain.ValueObjects;

public sealed class CreateOrderCommandHandler(
    IOrderRepository repository,
    IEntityNumberGenerator orderNumberGenerator)
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

        await repository.InsertAsync(order, cancellationToken);

        return order.Id;
    }
}