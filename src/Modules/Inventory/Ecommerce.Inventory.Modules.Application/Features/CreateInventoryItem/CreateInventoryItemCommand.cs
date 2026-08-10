using Ecommerce.Application.CQRS;

namespace Ecommerce.Inventory.Modules.Application.Features.CreateInventoryItem;

public sealed record CreateInventoryItemCommand(Guid ProductId, string SKU, int Quantity, int MinimumQuantity) : ICommand<Guid>;
