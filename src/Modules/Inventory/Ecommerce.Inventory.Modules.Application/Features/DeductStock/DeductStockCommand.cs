

namespace Ecommerce.Inventory.Modules.Application.Features.DeductStock;

public record DeductStockCommand(Guid InventoryItemId, int Quantity) : ICommand;

