
using Ecommerce.Inventory.Modules.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Ecommerce.Inventory.Modules.Domain.Tests;

public class InventoryItemTest
{
    [Fact]
    public void Reserve_Should_Decrease_AvailableQuantity()
    {
        var result = InventoryItem.Create(
            Guid.NewGuid(),
            "SKU-001",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        inventory.Reserve(30);

        inventory.OnHandQuantity.Should().Be(100);
        inventory.ReservedQuantity.Should().Be(30);
        inventory.AvailableQuantity.Should().Be(70);

        inventory.AvailableQuantity
            .Should()
            .Be(
                inventory.OnHandQuantity -
                inventory.ReservedQuantity);
    }
    [Fact]
    public void AvailableQuantity_Should_Be_OnHandQuantity_Minus_ReservedQuantity()
    {
        var result = InventoryItem.Create(
           Guid.NewGuid(),
           "SKU-001",
           quantity: 100,
           minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();
        var inventory = result.Value;
        inventory.AvailableQuantity
          .Should()
          .Be(
              inventory.OnHandQuantity -
              inventory.ReservedQuantity);

    }
}
