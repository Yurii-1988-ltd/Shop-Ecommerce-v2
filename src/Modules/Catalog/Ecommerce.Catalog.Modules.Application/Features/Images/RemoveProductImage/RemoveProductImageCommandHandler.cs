
namespace Ecommerce.Catalog.Modules.Application.Features.Images.RemoveProductImage;

internal sealed class RemoveProductImageCommandHandler(
    IProductRepository productRepository, IFileStorage filestorage)
    : ICommandHandler<RemoveProductImageCommand>
{
    public async Task<Result> Handle(
        RemoveProductImageCommand request,
        CancellationToken cancellationToken)
    {
       
        var product = await productRepository.GetByIdAsync(
            request.ProductId,
            cancellationToken);

        if (product is null)
            return ProductErrors.NotFound(request.ProductId);
        var image = product.Images.FirstOrDefault(x=>x.Id==request.ImageId);
        if(image is null)
            return ImagesErrors.NotFound(request.ImageId);
        var storageKey = image.StorageKey;

        var result = product.RemoveImage(request.ImageId);

        if (result.IsFailure)
            return result.Error;

        await productRepository.ReplaceAsync(
            product,
            cancellationToken);
        await filestorage.DeleteAsync(storageKey, cancellationToken);

        return Result.Success();
    }
}