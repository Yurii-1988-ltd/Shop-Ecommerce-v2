using Ecommerce.Catalog.Modules.Application.Features.Images.AddImage;

internal sealed class AddProductImageCommandHandler(IProductRepository productRepository) : ICommandHandler<AddProductImageCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return ProductErrors.NotFound(request.ProductId);

        var result = product.AddImage(request.StorageKey, request.AltText);

        if (result.IsFailure)
        {
            return Result<Guid>.Failure(result.Error);
        }

        // 2. Сохраняем агрегат в репозиторий
        await productRepository.ReplaceAsync(product, cancellationToken);

    
        var addedImage = product.Images.FirstOrDefault(i => i.StorageKey == request.StorageKey);

        if (addedImage is null)
        {
            // На случай, если в самом доменном объекте баг и коллекция не обновилась
            return Result<Guid>.Failure(new Error("Product.Images.Empty", "Image can not be added",ErrorType.Validation));
        }

        return addedImage.Id;
    }
}