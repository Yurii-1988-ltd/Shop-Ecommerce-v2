
using FluentValidation;

namespace Ecommerce.Cart.Modules.Application.Features.AddCartItem;

internal sealed class AddCartItemValidator: AbstractValidator<AddCartItemCommand>
{
    public AddCartItemValidator()
    {
        RuleFor(x=>x.CustomerId).NotEmpty();
        RuleFor(x=>x.ProductId).NotEmpty();
        RuleFor(x=>x.Name).NotEmpty()
            .MaximumLength(200);
        RuleFor(x=>x.Price).NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.Currency)
            .NotEmpty()
            .Length(3);
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}
