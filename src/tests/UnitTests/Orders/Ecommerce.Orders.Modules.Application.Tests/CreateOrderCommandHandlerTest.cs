

using Ecommerce.Application.Abstractions;

namespace Ecommerce.Orders.Modules.Application.Tests;

public sealed class CreateOrderCommandHandlerTest
{
    [Fact]
    public async Task Handle_Should_Create_Order_WhenCommandIsValid()
    {
        // 1. Arrange (Подготовка)
        var repositoryMock = new Mock<IOrderRepository>();
        var orderNumberGeneratorMock = new Mock<IEntityNumberGenerator>();

        // Настраиваем генератор номера заказа
        orderNumberGeneratorMock
            .Setup(x => x.GenerateAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("ORD-2026-100001");

        // Настраиваем InsertAsync в репозитории
        repositoryMock
            .Setup(x => x.InsertAsync(It.IsAny<Ecommerce.Order.Modules.Domain.Entities.Order>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateOrderCommandHandler(
            repositoryMock.Object,
            orderNumberGeneratorMock.Object);

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

        // Создаем команду с именованными параметрами
        var command = new CreateOrderCommand(
           Guid.NewGuid(),
        addressDto,
             itemsDto,
             CurrencyConstant.UAH);

        // 2. Act (Действие)
        var result = await handler.Handle(command, CancellationToken.None);

        // 3. Assert (Проверки)
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();

        repositoryMock.Verify(
            x => x.InsertAsync(
                It.Is<Ecommerce.Order.Modules.Domain.Entities.Order>(o => o.Items.Count == 1 && o.Currency == CurrencyConstant.UAH),
                It.IsAny<CancellationToken>()),
            Times.Once());
    }
}