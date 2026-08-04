using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Domain.Errors;

public  sealed class CartItemErrors
{
    public static Error InvalidPrice =>
        new("Invalid.Price", "The price is invalid",ErrorType.Validation);
    public static Error NameIsRequired =>
        new("Name.IsRequired", "The name is required", ErrorType.Validation);
    
    public static Error NegativePrice =>
        new("Negative.Price", "The Price can not be negative", ErrorType.Validation);

    public static Error NegativeAmount =>
        new("Negative.Amount", "The amount in stock cannot be negative.", ErrorType.Validation);
    public static Error CurrencyMismatch =>
        new Error("Currency.mismatch", "Currency is Mismatch", ErrorType.Validation);
    public static Error InvalidProductId =>
        new("Invalid.ProductId","Product is invalid",ErrorType.Validation);
  
    public static Error NegativeQuantity
        => new Error("Negative.Stock.Quantity", "Quantity in stock can not be nagative", ErrorType.Validation);
    public static Error NotFound(Guid id) =>
        Error.NotFound(
            "CartItem.NotFound",
            $"CartItem '{id}' was not found.");

    
}