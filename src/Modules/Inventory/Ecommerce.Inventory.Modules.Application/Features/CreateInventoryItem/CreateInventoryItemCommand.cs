using Ecommerce.Application.CQRS;

namespace Ecommerce.Inventory.Modules.Application.Features.CreateInventoryItem;

public sealed record CreateInventoryItemCommand(Guid ProductId, string Sku, int Quantity, int MinimumQuantity) : ICommand<Guid>;
