using Ecommerce.Domain.ValueObjects;
using Ecommerce.Cart.Modules.Domain.Entities;
using Ecommerce.Cart.Modules.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Ecommerce.Carts.Modules.Domain.Tests;

public class CouponTest
{
    [Fact]
    public void CalculateDiscount_Should_Apply_Percentage()
    {
        // Arrange
        var codeResult = CouponCode.Create("ECOMMERCE-#123-456-78");

        codeResult.IsSuccess.Should().BeTrue(
            because: $"Code of coupon is not valid: {codeResult.Error?.Description}");

        var minimumSpendResult = Money.Create(100, "UAH");

        minimumSpendResult.IsSuccess.Should().BeTrue(
            "MinimumSpend must be valid");

        var couponResult = PercentageCoupon.Create(
            codeResult.Value,
            DateTime.UtcNow.AddDays(1),
            minimumSpendResult.Value,
            0.5m);

        couponResult.IsSuccess.Should().BeTrue(
            because: $"Can not create coiupon {couponResult.Error?.Description}");

        var coupon = couponResult.Value;

        var subtotal = Money.Create(1000, "UAH").Value;

        // Act
        var discount = coupon.CalculateDiscount(subtotal);

        // Assert
        discount.Amount.Should().Be(500);
        discount.Currency.Should().Be("UAH");
    }


    [Fact]
    public void CalculateDiscount_Should_Respect_MaxDiscount()
    {
        // Arrange
        var codeResult = CouponCode.Create("ECOMMERCE-#123-456-78");

        codeResult.IsSuccess.Should().BeTrue(
            because: $"Code of coupon is not valid: {codeResult.Error?.Description}");

        var minimumSpend = Money.Create(100, "UAH");

        minimumSpend.IsSuccess.Should().BeTrue();

        var maxDiscount = Money.Create(300, "UAH");

        maxDiscount.IsSuccess.Should().BeTrue();

        var couponResult = PercentageCoupon.Create(
            codeResult.Value,
            DateTime.UtcNow.AddDays(1),
            minimumSpend.Value,
            0.5m,
            maxDiscount.Value);

        couponResult.IsSuccess.Should().BeTrue(
            because: $"купон не создался: {couponResult.Error?.Description}");

        var coupon = couponResult.Value;

        var subtotal = Money.Create(1000, "UAH").Value;

        // Act
        var discount = coupon.CalculateDiscount(subtotal);

        // Assert
        discount.Amount.Should().Be(300);
        discount.Currency.Should().Be("UAH");
    }
    [Fact]
    public void CalculateDiscount_Should_Return_Zero_When_MinimumSpend_Not_Met()
    {
        // Arrange
        var codeResult = CouponCode.Create("ECOMMERCE-#123-456-78");

        codeResult.IsSuccess.Should().BeTrue();

        var minimumSpend = Money.Create(1000, "UAH").Value;

        var couponResult = PercentageCoupon.Create(
            codeResult.Value,
            DateTime.UtcNow.AddDays(1),
            minimumSpend,
            0.5m);

        couponResult.IsSuccess.Should().BeTrue();

        var coupon = couponResult.Value;

        var subtotal = Money.Create(500, "UAH").Value;

        // Act
        var discount = coupon.CalculateDiscount(subtotal);

        // Assert
        discount.Amount.Should().Be(0);
        discount.Currency.Should().Be("UAH");
    }
}
