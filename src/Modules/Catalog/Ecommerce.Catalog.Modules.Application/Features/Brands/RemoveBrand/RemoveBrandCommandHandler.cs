using Ecommerce.Application.CQRS;
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Catalog.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.RemoveBrand;

internal sealed class RemoveBrandCommandHandler(IBrandRepository brandRepository) : ICommandHandler<RemoveBrandCommand>
{
    public async Task<Result> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await brandRepository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
            return CategoryErrors.NotFound(request.Id);


        await brandRepository.DeleteAsync(request.Id, cancellationToken);
        return Result.Success();
    }
}
