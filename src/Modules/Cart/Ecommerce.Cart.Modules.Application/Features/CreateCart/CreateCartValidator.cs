
using FluentValidation;

namespace Ecommerce.Cart.Modules.Application.Features.CreateCart;

internal sealed class CreateCartValidator: AbstractValidator<CreateCartCommand>
{
    public CreateCartValidator()
    {
        RuleFor(x=>x.CustomerId).NotEmpty();
    }
}
