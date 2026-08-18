

using FluentValidation;

namespace Ecommerce.Basket.Modules.Application.Features.AddItemToBasket
{
    internal sealed class AddItemToBasketValidator: AbstractValidator<AddItemToBasketCommand>
    {
        public AddItemToBasketValidator()
        {
            RuleFor(x=>x.CustomerId).NotEmpty();
            RuleFor(x=>x.ProductId).NotEmpty();
            RuleFor(x => x.UnitPrice).NotNull()
                .WithMessage("Unit price is required")
                .Must(price => price.Amount > 0)
                .WithMessage("Unit amount must be grater than zero")
                .Must(price => !string.IsNullOrWhiteSpace(price.Currency) && price.Currency.Length == 3)
                .WithMessage("UnitPrice currency must be a valid 3-character ISO code.");
                
        }
    }
}
