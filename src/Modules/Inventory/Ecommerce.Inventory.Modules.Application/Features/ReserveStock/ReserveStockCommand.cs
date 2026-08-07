

namespace Ecommerce.Inventory.Modules.Application.Features.ReserveStock;

public record ReserveStockCommand(Guid InventoryItemId, int Quantity) : ICommand;
