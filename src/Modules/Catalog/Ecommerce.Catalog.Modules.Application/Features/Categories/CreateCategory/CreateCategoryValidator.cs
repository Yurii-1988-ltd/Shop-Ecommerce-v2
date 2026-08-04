
using FluentValidation;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.CreateCategory;

internal sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x=>x.Name).NotEmpty()
            .MaximumLength(100);
        RuleFor(x=>x.Description).NotEmpty()
            .MaximumLength(500);
    }
}
