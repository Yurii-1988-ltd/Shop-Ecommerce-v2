

using Ecommerce.Application.CQRS;
 using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Catalog.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.CreateBrand;

internal sealed class CreateBrandCommandHandler(IBrandRepository brandRepository) : IQueryHandler<CreateBrandCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandResult = Brand.Create(request.Name,
                                             request.Description
                                            );
        if (brandResult.IsFailure)
            return brandResult.Error;
        await brandRepository.InsertAsync(brandResult.Value, cancellationToken);
        return brandResult.Value.Id;
    }
}
