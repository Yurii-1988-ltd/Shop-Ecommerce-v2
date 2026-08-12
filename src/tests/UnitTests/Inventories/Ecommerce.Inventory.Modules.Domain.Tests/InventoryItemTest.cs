
using Ecommerce.Inventory.Modules.Domain.Entities;
using Ecommerce.Inventory.Modules.Domain.Errors;
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
    [Fact]
    public void Inventory_operations_should_maintain_available_quantity_invariant()
    {
        var result = InventoryItem.Create(
            Guid.NewGuid(),
            "SKU-001",
            quantity: 100,
            minimumQuantity: 10);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        inventory.Reserve(30);

        inventory.AvailableQuantity
            .Should()
            .Be(inventory.OnHandQuantity - inventory.ReservedQuantity);

        inventory.CancelReservation(10);

        inventory.AvailableQuantity
            .Should()
            .Be(inventory.OnHandQuantity - inventory.ReservedQuantity);

        inventory.Reserve(20);

        inventory.AvailableQuantity
            .Should()
            .Be(inventory.OnHandQuantity - inventory.ReservedQuantity);

        inventory.CommitReservation(15);

        inventory.AvailableQuantity
            .Should()
            .Be(inventory.OnHandQuantity - inventory.ReservedQuantity);

        inventory.Replenish(50);

        inventory.AvailableQuantity
            .Should()
            .Be(inventory.OnHandQuantity - inventory.ReservedQuantity);

        inventory.Deduct(10);

        inventory.AvailableQuantity
            .Should()
            .Be(inventory.OnHandQuantity - inventory.ReservedQuantity);
    }
    [Fact]
    public void CancelReservation_Should_Restore_AvailableQuantity()
    {
        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: "SKU-001",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        // Сначала резервируем 30
        var reserveResult = inventory.Reserve(30);

        reserveResult.IsSuccess.Should().BeTrue();

        // Отменяем 10
        var cancelResult = inventory.CancelReservation(10);

        cancelResult.IsSuccess.Should().BeTrue();

        inventory.OnHandQuantity.Should().Be(100);
        inventory.ReservedQuantity.Should().Be(20);
        inventory.AvailableQuantity.Should().Be(80);

        // Проверяем инвариант
        inventory.AvailableQuantity
            .Should()
            .Be(
                inventory.OnHandQuantity -
                inventory.ReservedQuantity);
    }
    [Fact]
    public void CancelReservation_Should_Fail_When_ReservedQuantity_Is_Insufficient()
    {
        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: "SKU-001",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        inventory.Reserve(20);

        var cancelResult = inventory.CancelReservation(30);

        cancelResult.IsFailure.Should().BeTrue();
        cancelResult.Error.Should().Be(InventoryErrors.NotEnoughReservedStock);

        inventory.OnHandQuantity.Should().Be(100);
        inventory.ReservedQuantity.Should().Be(20);
        inventory.AvailableQuantity.Should().Be(80);
    }
    [Fact]
    public void CommitReservation_Should_Decrease_OnHand_And_Reserved()
    {
        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: "SKU-001",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        inventory.Reserve(30);

        var commitResult = inventory.CommitReservation(20);

        commitResult.IsSuccess.Should().BeTrue();

        inventory.OnHandQuantity.Should().Be(80);
        inventory.ReservedQuantity.Should().Be(10);
        inventory.AvailableQuantity.Should().Be(70);

        inventory.AvailableQuantity
            .Should()
            .Be(
                inventory.OnHandQuantity -
                inventory.ReservedQuantity);
    }
    [Fact]
    public void CommitReservation_Should_Fail_When_ReservedQuantity_Is_Insufficient()
    {
        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: "SKU-001",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        inventory.Reserve(20);

        var commitResult = inventory.CommitReservation(30);

        commitResult.IsFailure.Should().BeTrue();
        commitResult.Error.Should()
            .Be(InventoryErrors.NotEnoughReservedStock);

        inventory.OnHandQuantity.Should().Be(100);
        inventory.ReservedQuantity.Should().Be(20);
        inventory.AvailableQuantity.Should().Be(80);
    }
    [Fact]
    public void Replenish_Should_Increase_OnHandQuantity()
    {
        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: "SKU-001",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        var replenishResult = inventory.Replenish(50);

        replenishResult.IsSuccess.Should().BeTrue();

        inventory.OnHandQuantity.Should().Be(150);
        inventory.ReservedQuantity.Should().Be(0);
        inventory.AvailableQuantity.Should().Be(150);

        inventory.AvailableQuantity
            .Should()
            .Be(
                inventory.OnHandQuantity -
                inventory.ReservedQuantity);
    }
    [Fact]
    public void Replenish_Should_Increase_AvailableQuantity_When_Stock_Is_Reserved()
    {
        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: "SKU-001",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        inventory.Reserve(30);

        var replenishResult = inventory.Replenish(50);

        replenishResult.IsSuccess.Should().BeTrue();

        inventory.OnHandQuantity.Should().Be(150);
        inventory.ReservedQuantity.Should().Be(30);
        inventory.AvailableQuantity.Should().Be(120);

        inventory.AvailableQuantity
            .Should()
            .Be(
                inventory.OnHandQuantity -
                inventory.ReservedQuantity);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Replenish_Should_Fail_When_Quantity_Is_Not_Positive(int quantity)
    {
        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: "SKU-001",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventory = result.Value;

        var replenishResult = inventory.Replenish(quantity);

        replenishResult.IsFailure.Should().BeTrue();
        replenishResult.Error.Should()
            .Be(InventoryErrors.InvalidQuantity);

        inventory.OnHandQuantity.Should().Be(100);
        inventory.ReservedQuantity.Should().Be(0);
        inventory.AvailableQuantity.Should().Be(100);
    }
}
