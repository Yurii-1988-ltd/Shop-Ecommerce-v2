

namespace Ecommerce.Inventory.Modules.Application.Tests;

public class CreateInventoryItemCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_InventoryItem()
    {
        var repository = new Mock<IInventoryRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateInventoryItemCommandHandler(
            repository.Object,
            unitOfWork.Object);

        var productId = Guid.NewGuid();

        var command = new CreateInventoryItemCommand(
            productId,
            "SKU-001",
            100,
            5);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);

        repository.Verify(
            x => x.AddAsync(
                It.Is<InventoryItem>(item =>
                    item.ProductId == productId &&
                    item.SKU == "SKU-001" &&
                    item.OnHandQuantity == 100 &&
                    item.ReservedQuantity == 0 &&
                    item.MinimumQuantity == 5),
                It.IsAny<CancellationToken>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
    [Fact]
    public async Task Handle_Should_Return_Error_When_ProductId_Is_Empty()
    {
        // Arrange
        var repository = new Mock<IInventoryRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateInventoryItemCommandHandler(
            repository.Object,
            unitOfWork.Object);

        var command = new CreateInventoryItemCommand(
            Guid.Empty,
            "SKU-001",
            100,
            5);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(InventoryErrors.InvalidProductId);

        repository.Verify(
            x => x.AddAsync(
                It.IsAny<InventoryItem>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Handle_Should_Return_Error_When_Sku_Is_Empty(
     string? sku)
    {
        // Arrange
        var repository = new Mock<IInventoryRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateInventoryItemCommandHandler(
            repository.Object,
            unitOfWork.Object);

        var command = new CreateInventoryItemCommand(
            Guid.NewGuid(),
            sku!,
            100,
            5);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(InventoryErrors.InvalidSku);

        repository.Verify(
            x => x.AddAsync(
                It.IsAny<InventoryItem>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task Handle_Should_Return_Error_When_Quantity_Is_Invalid(
    int quantity)
    {
        // Arrange
        var repository = new Mock<IInventoryRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateInventoryItemCommandHandler(
            repository.Object,
            unitOfWork.Object);

        var command = new CreateInventoryItemCommand(
            Guid.NewGuid(),
            "SKU-001",
            quantity,
            5);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(InventoryErrors.InvalidQuantity);

        repository.Verify(
            x => x.AddAsync(
                It.IsAny<InventoryItem>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task Handle_Should_Return_Error_When_MinimumQuantity_Is_Invalid(
    int minimumQuantity)
    {
        // Arrange
        var repository = new Mock<IInventoryRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateInventoryItemCommandHandler(
            repository.Object,
            unitOfWork.Object);

        var command = new CreateInventoryItemCommand(
            Guid.NewGuid(),
            "SKU-001",
            100,
            minimumQuantity);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should()
            .Be(InventoryErrors.InvalidMinimumQuantity);

        repository.Verify(
            x => x.AddAsync(
                It.IsAny<InventoryItem>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }


}

