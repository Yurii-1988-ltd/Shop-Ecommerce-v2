

using Ecommerce.Application.CQRS;

namespace Ecommerce.Inventory.Modules.Application.Features.CommitReservation;

public record CommitReservationCommand(Guid inventoryItemId, int Quantity) : ICommand;

