

namespace Ecommerce.Catalog.Modules.Application.Features.Products.RemoveProduct
{
    internal sealed class RemomeProductCommandHandler(IProductRepository productRepository) : ICommandHandler<RemoveProductCommand>
    {
        public async Task<Result> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
                return ProductErrors.NotFound(request.Id);


            await productRepository.DeleteAsync(request.Id,cancellationToken);
            return Result.Success();



        }
    }
}
