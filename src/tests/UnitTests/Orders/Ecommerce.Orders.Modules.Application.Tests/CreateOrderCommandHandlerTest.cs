using Ecommerce.Application.Abstractions;
using Ecommerce.Shared.Contracts.IntegrationEvent;
using MassTransit;

namespace Ecommerce.Orders.Modules.Application.Tests;

public sealed class CreateOrderCommandHandlerTest
{
    [Fact]
    public async Task Handle_Should_Create_Order_And_Publish_Event_When_CommandIsValid()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();
        var orderNumberGeneratorMock = new Mock<IEntityNumberGenerator>();
        var publishEndpointMock = new Mock<IPublishEndpoint>();

        const string orderNumber = "ORD-2026-100001";
        const string customerEmail = "test@example.com";

        var customerId = Guid.NewGuid();

        orderNumberGeneratorMock
            .Setup(x => x.GenerateAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(orderNumber);

        repositoryMock
            .Setup(x => x.InsertAsync(
                It.IsAny<Order.Modules.Domain.Entities.Order>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateOrderCommandHandler(
            repositoryMock.Object,
            orderNumberGeneratorMock.Object,
            publishEndpointMock.Object);

        var addressDto = new OrderAddressDto(
            "Ivan",
            "Ivanov",
            "Ukraine",
            "Kyiv",
            "Khreshchatyk 1",
            "01001");

        var itemsDto = new List<CreateOrderItemDto>
        {
            new(
                ProductId: Guid.NewGuid(),
                ProductName: "Test Product",
                Sku: "TST-001",
                UnitPrice: 150.00m,
                Quantity: 2)
        };

        var command = new CreateOrderCommand(
            customerId,
            customerEmail,
            addressDto,
            itemsDto,
            CurrencyConstant.UAH);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();

        repositoryMock.Verify(
            x => x.InsertAsync(
                It.Is<Order.Modules.Domain.Entities.Order>(o =>
                    o.Items.Count == 1 &&
                    o.Currency == CurrencyConstant.UAH &&
                    o.CustomerId == customerId),
                It.IsAny<CancellationToken>()),
            Times.Once);

        publishEndpointMock.Verify(
            x => x.Publish(
                It.Is<OrderCreatedIntegrationEvent>(e =>
                    e.CustomerId == customerId &&
                    e.CustomerEmail == customerEmail &&
                    e.TotalAmount == 300.00m &&
                    e.OrderId == result.Value),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}