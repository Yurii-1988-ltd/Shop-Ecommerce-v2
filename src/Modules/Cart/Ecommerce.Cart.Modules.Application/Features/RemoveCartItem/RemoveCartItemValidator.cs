
using FluentValidation;

namespace Ecommerce.Cart.Modules.Application.Features.RemoveCartItem;

internal sealed class RemoveCartItemValidator: AbstractValidator<RemoveCartItemCommand>
{
    public RemoveCartItemValidator()
    {
        RuleFor(x=>x.ProductId).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
    }
}
