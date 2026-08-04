

using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Ecommerce.Catalog.Modules.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void Create_Should_Create_Product()
    {
        var money = Money.Create(100m, "USD").Value;
        var result = Product.Create(
                    "iphone 16",
                    "PRD-2026-004588",
                    "IPH-16",
                   money);
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be("iphone 16");
        result.Value.ProductNumber.Should().Be("");
        result.Value.Sku.Should().Be("IPH-16");
        result.Value.Price.Amount.Should().Be(100m);
        result.Value.Price.Currency.Should().Be("USD");

        result.Value.IsActive.Should().BeTrue();
    }
    [Fact]
    public void Create_Should_Return_Failure_When_Name_Is_Empty()
    {
        // Arrange
        var money = Money.Create(100m, "USD").Value;

        // Act
        var result = Product.Create(
            "",
            "PRD-2026-004588",
            "IPH-16",
            money);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProductErrors.NameIsRequired);
    }
    [Fact]
    public void Create_Should_Return_Failure_When_SKu_Is_Empty()
    {
        // Arrange
        var money = Money.Create(100m, "USD").Value;

        // Act
        var result = Product.Create(
            "iphone 16",
            "PRD-2026-004588",
            "",
            money);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProductErrors.SkuIsRequired);
    }
    [Fact]
  
    public void Create_Should_Return_Failure_When_Amount_Is_Negative()
    {
        // Arrange & Act
        var result = Money.Create(-100m, "USD");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MoneyErrors.NegativeAmount);
    }
}
