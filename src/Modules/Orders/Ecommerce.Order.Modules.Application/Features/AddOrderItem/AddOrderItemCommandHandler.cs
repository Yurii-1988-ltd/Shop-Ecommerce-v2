using Ecommerce.Order.Modules.Application.Features.AddOrderItem;

public sealed class AddOrderItemCommandHandler(
    IOrderRepository orderRepository,
    IProductService productService)
    : ICommandHandler<AddOrderItemCommand>
{
    public async Task<Result> Handle(
        AddOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(
            request.OrderId,
            cancellationToken);

        if (order is null)
            return OrderErrors.NotFound(request.OrderId);

        var productResult = await productService.GetProductAsync(
            request.ProductId,
            cancellationToken);

        if (productResult.IsFailure)
            return productResult.Error;

        var product = productResult.Value;

        var result = order.AddItem(
            product.Id,
            product.Name,
            product.Sku,
            product.Price,
            request.Quantity);

        if (result.IsFailure)
            return result.Error;

        await orderRepository.UpdateAsync(
            order,
            cancellationToken);

        return Result.Success();
    }
}