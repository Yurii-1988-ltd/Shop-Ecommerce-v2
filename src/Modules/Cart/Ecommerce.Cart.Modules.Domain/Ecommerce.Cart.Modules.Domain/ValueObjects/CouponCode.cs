using System.Text.RegularExpressions;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Domain.ValueObjects;

public partial record CouponCode
{
    [GeneratedRegex(
        @"^ECOMMERCE-#[0-9]{3}-[0-9]{3}-[0-9]{2}$",
        RegexOptions.IgnoreCase)]
    private static partial Regex CouponPattern();

    public string Value { get; }

    private CouponCode(string value)
    {
        Value = value.ToUpperInvariant();
    }

    public static Result<CouponCode> Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return Result.Failure<CouponCode>(
                new Error(
                    "CouponCode.Empty",
                    "Coupon code cannot be empty.",
                    ErrorType.Validation));
        }

        var normalizedCode = code.Trim();

        if (!CouponPattern().IsMatch(normalizedCode))
        {
            return Result.Failure<CouponCode>(
                new Error(
                    "CouponCode.InvalidFormat",
                    "Invalid coupon format. Expected ECOMMERCE-#123-456-78.",
                    ErrorType.Validation));
        }

        return new CouponCode(normalizedCode);
    }

    public static CouponCode GenerateNew()
    {
        return new CouponCode(
            $"ECOMMERCE-#{Random.Shared.Next(100, 1000)}-" +
            $"{Random.Shared.Next(100, 1000)}-" +
            $"{Random.Shared.Next(10, 100)}");
    }

    public static implicit operator string(CouponCode code) => code.Value;
}