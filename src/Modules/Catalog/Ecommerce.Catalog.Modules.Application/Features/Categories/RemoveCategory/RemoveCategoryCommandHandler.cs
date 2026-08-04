
using Ecommerce.Application.CQRS;
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Catalog.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.RemoveCategory;

internal sealed class RemoveCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<RemoveCategoryCommand>
{
    public async Task<Result> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
            return CategoryErrors.NotFound(request.Id);


        await categoryRepository.DeleteAsync(request.Id, cancellationToken);
        return Result.Success();
    }
}
