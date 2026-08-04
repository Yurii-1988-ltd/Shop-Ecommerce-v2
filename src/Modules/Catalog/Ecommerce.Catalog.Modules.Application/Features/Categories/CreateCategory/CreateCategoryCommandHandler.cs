
using Ecommerce.Application.CQRS;
using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Catalog.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;


namespace Ecommerce.Catalog.Modules.Application.Features.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
       
        var categoryResult = Category.Create(request.Name,
                                            request.Description
                                           );
        if (categoryResult.IsFailure)
            return categoryResult.Error;
        await categoryRepository.InsertAsync(categoryResult.Value, cancellationToken);
        return categoryResult.Value.Id;
    }
}
