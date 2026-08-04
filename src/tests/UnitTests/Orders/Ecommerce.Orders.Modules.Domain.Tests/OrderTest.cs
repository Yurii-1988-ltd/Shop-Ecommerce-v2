


using Ecommerce.Domain.Constants;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Order.Modules.Domain.Enums;
using Ecommerce.Order.Modules.Domain.Errors;
using Ecommerce.Order.Modules.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Ecommerce.Orders.Modules.Domain.Tests;


public sealed class OrderTests
{
    [Fact]
    public void Create_Should_Create_Order()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        var address = new OrderAddress(
            "Ivan",
            "Ivanov",
            "Ukraine",
            "Kyiv",
            "Khreshchatyk 1",
            "01001");

        // Act
        var result = Order.Modules.Domain.Entities.Order.Create(customerId, "1E96E7DB-E54A-4DBD-A3C8-7A3B754DC809", address);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var order = result.Value;

        order.CustomerId.Should().Be(customerId);
        order.ShippingAddress.Should().Be(address);
        order.Currency.Should().Be(CurrencyConstant.UAH);
        order.Items.Should().BeEmpty();
    }

    [Fact]
    public void Create_Should_Return_Failure_When_CustomerId_Is_Empty()
    {
        // Arrange
        var address = new OrderAddress(
            "Ivan",
            "Ivanov",
            "Ukraine",
            "Kyiv",
            "Khreshchatyk 1",
            "01001");

        // Act
        var result = Order.Modules.Domain.Entities.Order.Create(Guid.Empty, "ORD-2026-000001", address);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(OrderErrors.GuidEmpty);
    }
    [Fact]
    public void Create_Should_Create_Order_With_Draft_Status()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        var address = new OrderAddress(
          "Ivan",
          "Ivanov",
          "Ukraine",
          "Kyiv",
          "Khreshchatyk 1",
          "01001");
        // Act
        var result = Order.Modules.Domain.Entities.Order.Create(customerId, "ORD-2026-000001",address);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var order = result.Value;

        order.CustomerId.Should().Be(customerId);
        order.Status.Should().Be(OrderStatus.Pending);
        order.OrderNumber.Should().Be("ORD-2026-000001");
        order.Items.Should().BeEmpty();
    }
    [Fact]
    
    public void ChangeItemQuantity_Should_Change_Quantity()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        var address = new OrderAddress(
            "Ivan",
            "Ivanov",
            "Ukraine",
            "Kyiv",
            "Khreshchatyk 1",
            "01001");

        var order = Order.Modules.Domain.Entities.Order.Create(customerId, "ORD-2026-000001", address).Value;

        var price = Money.Create(100, CurrencyConstant.UAH).Value;

        order.AddItem(
            Guid.NewGuid(),
            "iPhone 16",
            "IP-001",
            price,
            1);

        var item = order.Items.First();

        // Act
        var result = order.ChangeItemQuantity(item.Id, 5);

        // Assert
        result.IsSuccess.Should().BeTrue();
        item.Quantity.Should().Be(5);
    }


}

