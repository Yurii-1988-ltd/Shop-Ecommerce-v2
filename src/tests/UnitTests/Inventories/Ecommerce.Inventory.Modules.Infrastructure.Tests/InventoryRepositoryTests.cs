

using Ecommerce.Inventory.Modules.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ecommerce.Inventory.Modules.Infrastructure.Tests;

public class InventoryRepositoryTests
{
    [Fact]
    public async Task AddAsync_Should_Add_InventoryItem()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InventoryContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=inventories;Username=postgres;Password=postgres")
            .Options;

        await using var context = new InventoryContext(options);

        var repository = new InventoryRepository(context);

        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: $"TEST-{Guid.NewGuid()}",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventoryItem = result.Value;

        // Act
        await repository.AddAsync(
            inventoryItem,
            CancellationToken.None);

        await context.SaveChangesAsync();

        // Assert
        var savedItem = await context.Inventories
            .FirstOrDefaultAsync(x => x.Id == inventoryItem.Id);

        savedItem.Should().NotBeNull();

        savedItem!.ProductId.Should().Be(inventoryItem.ProductId);
        savedItem.SKU.Should().Be(inventoryItem.SKU);
        savedItem.OnHandQuantity.Should().Be(100);
        savedItem.ReservedQuantity.Should().Be(0);
        savedItem.MinimumQuantity.Should().Be(5);
    }
    [Fact]
    public async Task GetAsync_Should_Return_InventoryItem()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InventoryContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=inventories;Username=postgres;Password=postgres")
            .Options;

        await using var context = new InventoryContext(options);

        var repository = new InventoryRepository(context);

        var result = InventoryItem.Create(
            productId: Guid.NewGuid(),
            sku: $"TEST-{Guid.NewGuid()}",
            quantity: 100,
            minimumQuantity: 5);

        result.IsSuccess.Should().BeTrue();

        var inventoryItem = result.Value;

        await repository.AddAsync(
            inventoryItem,
            CancellationToken.None);

        await context.SaveChangesAsync();

        // Act
        var actual = await repository.GetAsync(
            inventoryItem.Id,
            CancellationToken.None);

        // Assert
        actual.Should().NotBeNull();

        actual!.Id.Should().Be(inventoryItem.Id);
        actual.ProductId.Should().Be(inventoryItem.ProductId);
        actual.SKU.Should().Be(inventoryItem.SKU);
        actual.OnHandQuantity.Should().Be(100);
        actual.ReservedQuantity.Should().Be(0);
        actual.MinimumQuantity.Should().Be(5);

        actual.AvailableQuantity
            .Should()
            .Be(
                actual.OnHandQuantity -
                actual.ReservedQuantity);
    }
    [Fact]
    public async Task GetAsync_Should_Return_Null_When_InventoryItem_Does_Not_Exist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InventoryContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=inventories;Username=postgres;Password=postgres")
            .Options;

        await using var context = new InventoryContext(options);

        var repository = new InventoryRepository(context);

        // Act
        var result = await repository.GetAsync(
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
