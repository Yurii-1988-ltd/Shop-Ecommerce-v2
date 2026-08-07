
using Ecommerce.Domain.Domain;

namespace Ecommerce.Inventory.Modules.Domain.Errors;

public  class InventoryErrors
{
    public static Error NotEnoughStock
        => new Error("Not.Enough.Stock", "Not enough stock available.", ErrorType.Validation);
    public static Error InvalidQuantity
        => new Error("Invalid.Quantity", "Quantity must be greater than zero.", ErrorType.Validation);
        public static Error NotEnoughReservedStock=>
        new Error("Not.Enough.Reserved.Stock", "Not enough reserved stock available.", ErrorType.Validation);
    public static Error NotFound(Guid id)
        => new Error("Inventory.Item.Not.Found", $"Inventory item with ID '{id}' was not found.", ErrorType.NotFound);
}
