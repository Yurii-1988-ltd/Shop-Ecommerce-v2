using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Inventory.Modules.Application.Features.CancelReservation
{
    internal sealed class CancelReservationValidation: AbstractValidator<CancelReservationCommand>
    {
        public CancelReservationValidation()
        {
            RuleFor(x => x.InventoryItemId)
                .NotEmpty().WithMessage("Inventory item id is required.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be a positive number.");
        }
    }
}
