

using Ecommerce.Domain.Domain;
using Ecommerce.Inventory.Modules.Domain.Errors;
using Ecommerce.Inventory.Modules.Domain.Events;

namespace Ecommerce.Inventory.Modules.Domain.Entities;

public sealed class InventoryItem : Entity
{
    #region properties
    public Guid ProductId { get;private set; }
    public string SKU { get; private set; }
    public int AvailableQuantity { get; private set; }
    public int ReservedQuantity { get;private set; }
    public int MinimumQuantity { get;private set; }
    public DateTime UpdatedUtc { get;private set; }
    public int OnHandQuantity => AvailableQuantity + ReservedQuantity;
    #endregion
    private InventoryItem()
    {
        
    }
    public InventoryItem(Guid productId, string sku, int availableQuantity, int reservedQuantity, int minimumQuantity, DateTime updatedUtc)
    {
        ProductId = productId;
        SKU = sku;
        AvailableQuantity = availableQuantity;
        ReservedQuantity = reservedQuantity;
        MinimumQuantity = minimumQuantity;
        UpdatedUtc = updatedUtc;
    }

    #region static factory methods

    public Result Reserve(int quantity)
    {
        if(quantity<=0)
            return InventoryErrors.NotEnoughStock;
        AvailableQuantity += quantity;
        ReservedQuantity += quantity;
        UpdatedUtc = DateTime.UtcNow;
        return Result.Success();
    }
    public Result Deduct(int quantity)
    {
        if (quantity <= 0)
            return InventoryErrors.InvalidQuantity;

        if (AvailableQuantity < quantity)
            return InventoryErrors.NotEnoughStock;

        AvailableQuantity -= quantity;

        UpdatedUtc = DateTime.UtcNow;

        AddDomainEvent(
            new StockDeductedDomainEvent(
                Id,
                ProductId,
                quantity));

        return Result.Success();
    }


    #endregion

}
