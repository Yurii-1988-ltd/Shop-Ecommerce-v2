

using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Order.Modules.Application.Contracts;
using Ecommerce.Order.Modules.Application.Features.AddOrderItem;
using Ecommerce.Order.Modules.Application.Services;
using Ecommerce.Order.Modules.Domain.Errors;

namespace Ecommerce.Orders.Modules.Application.Tests;

public sealed class AddOrderItemCommandHandlerTest
{
    [Fact]
    public async Task Handle_Should_Add_Order_Item()
    {
        // Arrange
        var repository = new Mock<IOrderRepository>();
        var productService = new Mock<IProductService>();

        var address = new OrderAddress(
            "Ivan",
            "Ivanov",
            "Ukraine",
            "Kyiv",
            "Street",
            "01001");

        var order = Order.Modules.Domain.Entities.Order.Create(Guid.NewGuid(), "ORD-2026-000001", address).Value;

        repository
            .Setup(x => x.GetByIdAsync(
                order.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var product = new ProductInfo(
            Guid.NewGuid(),
            "iPhone",
            "IP-001",
            Money.Create(100, CurrencyConstant.UAH).Value);

        productService
            .Setup(x => x.GetProductAsync(
                product.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(product));

        repository
            .Setup(x => x.UpdateAsync(
                It.IsAny<Order.Modules.Domain.Entities.Order>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var handler = new AddOrderItemCommandHandler(
            repository.Object,
            productService.Object);

        var command = new AddOrderItemCommand(
            order.Id,
            product.Id,
            2);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        repository.Verify(
            x => x.UpdateAsync(
                It.IsAny<Order.Modules.Domain.Entities.Order>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
    [Fact]
    public async Task Handle_Should_Return_NotFound_When_Order_Does_Not_Exist()
    {
        // Arrange
        var repository = new Mock<IOrderRepository>();
        var productService = new Mock<IProductService>();

        var orderId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        repository
            .Setup(x => x.GetByIdAsync(
                orderId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order.Modules.Domain.Entities.Order?)null);

        var handler = new AddOrderItemCommandHandler(
            repository.Object,
            productService.Object);

        var command = new AddOrderItemCommand(
            orderId,
            productId,
            2);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(OrderErrors.NotFound(orderId));

        productService.Verify(
            x => x.GetProductAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        repository.Verify(
            x => x.UpdateAsync(
                It.IsAny<Order.Modules.Domain.Entities.Order>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}