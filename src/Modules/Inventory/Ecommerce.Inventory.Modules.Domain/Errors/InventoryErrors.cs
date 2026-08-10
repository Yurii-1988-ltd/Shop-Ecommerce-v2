
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
    public static Error InvalidProductId
        => new Error("Invalid.ProductId", "ProductId con not be empty", ErrorType.Validation);
    public static Error InvalidSku
        => new Error("Invalid.Sku", "Sku is required", ErrorType.Validation);
    public static Error InvalidMinimumQuantity
        => new Error("Invalid.MinimumQuantity", " MinimumQuantity must be greater than zero.", ErrorType.Validation);
}
