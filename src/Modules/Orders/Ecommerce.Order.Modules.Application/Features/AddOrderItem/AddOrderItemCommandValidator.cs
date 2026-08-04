using FluentValidation;

namespace Ecommerce.Order.Modules.Application.Features.AddOrderItem;

public sealed class AddOrderItemCommandValidator:AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId is required.");
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be a positive number.");
    }
}
