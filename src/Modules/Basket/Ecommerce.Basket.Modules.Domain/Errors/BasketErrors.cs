
using Ecommerce.Domain.Domain;

namespace Ecommerce.Basket.Modules.Domain.Errors;

public static class BasketErrors
{
    public static Error EmptyCustomerId
        => new Error("Empty.CustomerId", "CastomerId can not be empty", ErrorType.Validation);
    public static Error BasketNotActive
        => new Error("Basket.Not.Active", "The basket is not active", ErrorType.Validation);
    public static Error MaxDistinctItemsReached
        => new Error("Basket.MaxDistinctItems", "Basket cannot contain more than 50 unique items.", ErrorType.Validation);
    public static Error InvalidQuantityZeroOrNegative
        => new Error("Invalid.Quantity.Zero.Or.Negative", "Quantity can not be negative", ErrorType.Validation);
    public static Error QuantityExceedsLimit
        => new Error("Quantity.Exceeds.Limit", "Quantity exxeeds limit", ErrorType.Validation);
    public static Error EmptyProductId
        => new Error("Empty.ProductId", "ProductId can not be empty", ErrorType.Validation);
    public static Error EmptyProductName
      => new Error("Empty.ProductName", "Product name can not be empty", ErrorType.Validation);
    public static Error NullUnitPrice
        => new Error("Null.Unit Price", "Unit Price Cannot be Null", ErrorType.Validation);
    public static Error ItemNotFound
        => new Error("Item.NotFound", "Item not found", ErrorType.Validation);
    public static Error EmptyBasketCheckout =>
        new Error("Empty.Basket.Checkout", "Basket is empty", ErrorType.Validation);
    public static Error CouponExpired
        => new Error("Coupon.Expired", "Coupon is expired", ErrorType.Validation);
    public static Error CouponMinimumSpendNotMet
        => new Error("Coupon.Minimum.Spend.NotMet", "The minimum spend required to use this coupon has not been met.", ErrorType.Validation);
    public static Error InvalidQuantity =>
        new Error("Invalid.Quantity", "Quantity invalid", ErrorType.Validation);
}
