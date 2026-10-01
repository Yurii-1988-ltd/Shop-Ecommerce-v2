using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Errors;

public sealed class ProductErrors
{
    public static Error InvalidPrice =>
        new(
            "Invalid.Price",
            "The price is invalid",
            ErrorType.Validation);

    public static Error NameIsRequired =>
        new(
            "Name.IsRequired",
            "The name is required",
            ErrorType.Validation);

    public static Error SkuIsRequired =>
        new(
            "Sku.IsRequired",
            "The SKU is required",
            ErrorType.Validation);

    public static Error NegativePrice =>
        new(
            "Negative.Price",
            "The price cannot be negative",
            ErrorType.Validation);

    public static Error NegativeAmount =>
        new(
            "Negative.Amount",
            "The amount in stock cannot be negative",
            ErrorType.Validation);

    public static Error CurrencyMismatch =>
        new(
            "Currency.Mismatch",
            "Currency mismatch",
            ErrorType.Validation);

    public static Error InvalidSalePrice =>
        new(
            "Invalid.SalePrice",
            "The sale price is invalid",
            ErrorType.Validation);

    public static Error NegativeStockQuantity =>
        new(
            "Negative.StockQuantity",
            "Quantity in stock cannot be negative",
            ErrorType.Validation);

    public static Error ProductAlreadyArchived =>
        new(
            "Product.AlreadyArchived",
            "Product is already archived",
            ErrorType.Validation);

    public static Error NotFound(Guid id) =>
        Error.NotFound(
            "Products.NotFound",
            $"Product '{id}' was not found.");

    public static Error NotFound(string message) =>
        Error.NotFound(
            "Products.NotFound",
            message);
}