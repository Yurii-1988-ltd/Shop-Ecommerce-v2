

using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Errors;

public sealed class ProductErrors
{
    public static Error InvalidPrice =>
        new("Invalid.Proce", "The price is invalid",ErrorType.Validation);
    public static Error NameIsRequired =>
        new("Name.IsRequired", "The name is required", ErrorType.Validation);
    public static Error SkuIsRequired =>
        new("Sku.IsRequired", "The Sku is required", ErrorType.Validation);
    public static Error NagativePrice =>
    new("Nagative.Price", "The Price can not be nagative", ErrorType.Validation);

    public static Error NagativeAmount =>
 new("Nagative.Amount", "The amount in stock cannot be negative.", ErrorType.Validation);
    public static Error CurrencyMismatch =>
        new Error("Currencym.ismatch", "Currency is Mismatch", ErrorType.Validation);
    public static Error InvalidSalePrice =>
        new Error("Invalid.Sale.Price", "Sale Prise is Invalid",ErrorType.Validation);
    public static Error NegativeStockQuantity
        => new Error("Negative.Stock.Quantity", "Quantity in stock can not be nagative", ErrorType.Validation);
    public static Error ProductAlreadyArchived
        => new Error("Product.Already.Archived", "Product Already was Archived", ErrorType.Validation);
    public static Error NotFound(Guid id) =>
        Error.NotFound(
            "Products.NotFound",
            $"Product '{id}' was not found.");


}
