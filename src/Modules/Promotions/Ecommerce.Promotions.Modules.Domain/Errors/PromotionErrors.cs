using Ecommerce.Domain.Domain;

namespace Ecommerce.Promotions.Modules.Domain.Errors;

/// <summary>
/// Contains domain errors related to promotion and coupon operations.
/// </summary>
public static class PromotionErrors
{
    /// <summary>Occurs when the provided coupon code string is null, empty, or whitespace.</summary>
    public static readonly Error CouponCodeIsEmpty = Error.Validation(
        "Promotions.CouponCodeIsEmpty",
        "The coupon code cannot be empty.");

    /// <summary>Occurs when the coupon code format does not match the required pattern.</summary>
    public static readonly Error InvalidCouponCodeFormat = Error.Validation(
        "Promotions.InvalidCouponCodeFormat",
        "The coupon code format is invalid.");

    /// <summary>Occurs when no coupon with the specified code is found in the repository.</summary>
    public static Error CouponNotFound(string code) => Error.NotFound(
        "Promotions.CouponNotFound",
        $"Coupon with code '{code}' was not found.");

    /// <summary>Occurs when attempting to use a coupon whose expiration date has passed.</summary>
    public static readonly Error CouponExpired = Error.Validation(
        "Promotions.CouponExpired",
        "The coupon has expired.");

    /// <summary>Occurs when attempting to use a coupon that has been deactivated.</summary>
    public static readonly Error CouponNotActive = Error.Validation(
        "Promotions.CouponNotActive",
        "The coupon is not active.");

    /// <summary>Occurs when the coupon reached its maximum allowed number of redemptions.</summary>
    public static readonly Error UsageLimitExceeded = Error.Validation(
        "Promotions.UsageLimitExceeded",
        "The coupon usage limit has been reached.");

    /// <summary>Occurs when the provided order subtotal is negative or invalid.</summary>
    public static readonly Error InvalidSubtotal = Error.Validation(
        "Promotions.InvalidSubtotal",
        "The order subtotal must be greater than zero.");

    /// <summary>Occurs when the order subtotal does not meet the minimum amount required by the coupon.</summary>
    public static Error MinimumSpendNotReached(decimal minimumSpend) => Error.Validation(
        "Promotions.MinimumSpendNotReached",
        $"The minimum spend requirement of {minimumSpend} has not been met.");

    /// <summary>Occurs when the calculated or specified discount value is negative or invalid.</summary>
    public static readonly Error InvalidDiscountValue = Error.Validation(
        "Promotions.InvalidDiscountValue",
        "The specified discount value is invalid.");

    /// <summary>Occurs when a promotional campaign with the specified ID cannot be found.</summary>
    public static Error PromotionNotFound(Guid id) => Error.NotFound(
        "Promotions.NotFound",
        $"Promotion with ID '{id}' was not found.");

    /// <summary>Occurs when attempting to create a promotion or coupon that already exists.</summary>
    public static readonly Error PromotionAlreadyExists = Error.Conflict(
        "Promotions.AlreadyExists",
        "A promotion with the same name or code already exists.");
}