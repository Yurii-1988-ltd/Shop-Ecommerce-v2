using Ecommerce.Catalog.Modules.Application.Mapping;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.UpdateProduct;

internal  sealed class UpdateProductCommandHandler(IProductRepository repository) : ICommandHandler<UpdateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id,cancellationToken);

        if (product is null)
            return ProductErrors.NotFound(request.Id);
        var moneyResult = request.Price.ToMoney();

        if (moneyResult.IsFailure)
            return moneyResult.Error;



        var result = product.UpdateInformation(
            request.Name,
            request.Description,
            request.Sku);

        if (result.IsFailure)
            return result.Error;

        result = product.UpdatePrice(moneyResult.Value);

        if (result.IsFailure)
            return result.Error;

        await repository.ReplaceAsync(product,cancellationToken);

        return Result<Guid>.Success(product.Id);
    }
}
