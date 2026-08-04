using Ecommerce.Application.CQRS;
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Catalog.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.UpdateBrand;

internal sealed class UpdateBrandCommandHandler(IBrandRepository brandRepository) : ICommandHandler<UpdateBrandCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await brandRepository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
            return CategoryErrors.NotFound(request.Id);
        var result = brand.Update(request.Name, request.Description);

        if (result.IsFailure)
            return result.Error;
        await brandRepository.ReplaceAsync(brand, cancellationToken);
        return Result<Guid>.Success(brand.Id);


    }
}
