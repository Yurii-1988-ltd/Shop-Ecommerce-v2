
namespace Ecommerce.Catalog.Modules.Application.Features.Images.SetPrimaryImage;

internal sealed class SetPrimaryImageCommandHandler(IProductRepository productRepository)
    : ICommandHandler<SetPrimaryImageCommand>
{
    public async Task<Result> Handle(SetPrimaryImageCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return ProductErrors.NotFound(request.ProductId);
        var result = product.SetPrimaryImage(request.ImageId);
        if (result.IsFailure)
            return  result.Error;
       await productRepository.ReplaceAsync(product, cancellationToken);
        return Result.Success();
        
        
    }
}