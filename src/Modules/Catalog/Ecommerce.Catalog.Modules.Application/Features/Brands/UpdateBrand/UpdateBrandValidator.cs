
using FluentValidation;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.UpdateBrand;

internal sealed class UpdateBrandValidator: AbstractValidator<Categories.UpdateCategory.UpdateBrandCommand>
{
    public UpdateBrandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();

    }

}
