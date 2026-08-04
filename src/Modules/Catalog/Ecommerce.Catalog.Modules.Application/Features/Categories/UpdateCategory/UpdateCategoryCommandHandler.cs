
using Ecommerce.Application.CQRS;
using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Catalog.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.UpdateCategory;

internal sealed class UpdateBrandCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<UpdateBrandCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
            return CategoryErrors.NotFound(request.Id);
        var result = category.Update(request.Name, request.Description);

        if (result.IsFailure)
            return result.Error;
        await categoryRepository.ReplaceAsync(category, cancellationToken);
        return Result<Guid>.Success(category.Id);


    }
}
