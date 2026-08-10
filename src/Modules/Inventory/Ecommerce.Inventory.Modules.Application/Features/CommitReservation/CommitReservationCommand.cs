

using Ecommerce.Application.CQRS;

namespace Ecommerce.Inventory.Modules.Application.Features.CommitReservation;

public record CommitReservationCommand(Guid InventoryItemId, int Quantity) : ICommand;

