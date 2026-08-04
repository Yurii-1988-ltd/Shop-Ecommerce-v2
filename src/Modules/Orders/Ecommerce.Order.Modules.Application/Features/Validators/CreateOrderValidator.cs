
using Ecommerce.Domain.ValueObjects;

using FluentValidation;

namespace Ecommerce.Order.Modules.Application.Features.Validators;

internal sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x=>x.CustomerId).NotEmpty();
        RuleFor(x=>x.Currency).NotEmpty()
            .Must(currency=>Money.Create(0,currency).IsSuccess)
            .WithMessage("Invalid currency");
    }
}
