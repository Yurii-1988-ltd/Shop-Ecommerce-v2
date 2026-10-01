namespace Ecommerce.Catalog.Modules.Presentation;

internal static class ProductErrorLocalization
{
    public static string? GetTranslationKey(string errorCode)
    {
        return errorCode switch
        {
            "Name.IsRequired" => "Product.NameIsRequired",
            "Sku.IsRequired" => "Product.SkuIsRequired",
            "Invalid.Price" => "Product.InvalidPrice",
            "Negative.Price" => "Product.NegativePrice",
            "Negative.Amount" => "Product.NegativeAmount",
            "Currency.Mismatch" => "Product.CurrencyMismatch",
            "Invalid.SalePrice" => "Product.InvalidSalePrice",
            "Negative.StockQuantity" => "Product.NegativeStockQuantity",
            "Product.AlreadyArchived" => "Product.AlreadyArchived",
            "Products.NotFound" => "Product.NotFound",
            _ => null
        };
    }
}