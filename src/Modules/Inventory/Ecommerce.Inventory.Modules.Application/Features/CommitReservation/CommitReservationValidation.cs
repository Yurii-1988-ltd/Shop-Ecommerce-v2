

using FluentValidation;

namespace Ecommerce.Inventory.Modules.Application.Features.CommitReservation;

internal sealed class CommitReservationValidation : AbstractValidator<CommitReservationCommand>
{
    public CommitReservationValidation()
    {
        RuleFor(x => x.InventoryItemId).NotEmpty().WithMessage("Inventory item id is required.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
