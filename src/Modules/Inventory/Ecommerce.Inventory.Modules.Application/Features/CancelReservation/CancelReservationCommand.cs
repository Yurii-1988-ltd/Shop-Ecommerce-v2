
using Ecommerce.Application.CQRS;


namespace Ecommerce.Inventory.Modules.Application.Features.CancelReservation;

public sealed record CancelReservationCommand(Guid InventoryItemId, int Quantity) : ICommand;

