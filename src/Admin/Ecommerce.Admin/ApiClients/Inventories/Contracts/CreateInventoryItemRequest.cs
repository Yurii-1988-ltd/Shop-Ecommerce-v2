namespace Ecommerce.Admin.ApiClients.Inventories.Contracts
{
    public sealed record CreateInventoryItemRequest(Guid ProductId,
    string SKU,
    int Quantity,
    int MinimumQuantity);
  
}
