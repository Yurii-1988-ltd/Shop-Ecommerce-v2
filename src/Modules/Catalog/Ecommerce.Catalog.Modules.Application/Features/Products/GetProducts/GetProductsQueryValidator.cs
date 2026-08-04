using FluentValidation;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProducts;


internal sealed class GetProductsQueryValidator: AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}
