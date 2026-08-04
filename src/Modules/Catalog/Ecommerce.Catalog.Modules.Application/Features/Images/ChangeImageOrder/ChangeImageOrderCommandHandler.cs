
namespace Ecommerce.Catalog.Modules.Application.Features.Images.ChangeImageOrder;

internal sealed class ChangeImageOrderCommandHandler(IProductRepository productRepository): ICommandHandler<ChangeImageOrderCommand>
{
    public async Task<Result> Handle(ChangeImageOrderCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return ProductErrors.NotFound(request.ProductId);
        var result = product.ReorderImages(request.ImageId, request.NewIndex);
      if (result.IsFailure)
        return result;
      await productRepository.ReplaceAsync(product, cancellationToken);
        return Result.Success();
        
    }
}