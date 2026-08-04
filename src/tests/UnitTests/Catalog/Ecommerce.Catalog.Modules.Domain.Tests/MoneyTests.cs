using Ecommerce.Domain.Errors;
using Ecommerce.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Ecommerce.Catalog.Modules.Domain.Tests;

public sealed class MoneyTests
{
    [Fact]
    
    public void Add_Should_Return_Failure_When_Currencies_Are_Different()
    {
        // Arrange
        var usd = Money.Create(100m, "USD").Value;
        var eur = Money.Create(50m, "EUR").Value;

        // Act
        var result = usd.Add(eur);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MoneyErrors.CurrencyMismatch);
    }
    [Fact]
    public void Add_Should_Return_Success_When_Currencies_Are_Equal()
    {
        // Arrange
        var first = Money.Create(100m, "USD").Value;
        var second = Money.Create(50m, "USD").Value;

        // Act
        var result = first.Add(second);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Amount.Should().Be(150m);
        result.Value.Currency.Should().Be("USD");
    }
}
