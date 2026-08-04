
using FluentValidation;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.UpdateCategory;

internal sealed class UpdateBrandValidator: AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();

    }

}
