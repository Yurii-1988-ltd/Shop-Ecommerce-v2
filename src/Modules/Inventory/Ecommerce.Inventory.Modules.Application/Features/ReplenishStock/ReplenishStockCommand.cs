

namespace Ecommerce.Inventory.Modules.Application.Features.ReplenishStock;

public record ReplenishStockCommand(Guid InventoryItemId, int Quantity) : ICommand;

