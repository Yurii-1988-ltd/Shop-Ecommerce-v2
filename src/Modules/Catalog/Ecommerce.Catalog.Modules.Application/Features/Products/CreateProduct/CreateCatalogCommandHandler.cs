using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.CreateProduct;

public sealed class CreateCatalogCommandHandler(IProductRepository productRepository) : ICommandHandler<CreateCatalogCommand,Guid>
{

    public async Task<Result<Guid>> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
    {
        var money = Money.Create(request.Price, request.Currency);
        if (money.IsFailure)
            return money.Error;


        var productResult = Product.Create(request.Name,
                                            request.ProductNumber,
                                            request.Sku,
                                            money.Value,
                                            request.Description);
        if (productResult.IsFailure)
            return productResult.Error;
        await productRepository.InsertAsync(productResult.Value, cancellationToken);
        return productResult.Value.Id;


    }



}
