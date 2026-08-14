

using Ecommerce.Inventory.Modules.Application.Abstractions;
using Ecommerce.Inventory.Modules.Application.Features.GetInventoryReport;
using Ecommerce.Inventory.Modules.Application.Responses;

namespace Ecommerce.Inventory.Modules.Application.Tests;

public class GetInventoryReportQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_InventoryReport()
    {
        // Arrange
        var queries = new Mock<IInventoryQueries>();

        var items = new List<InventoryReportItem>
{
    new(
        Guid.NewGuid(),
        Guid.NewGuid(),
        "SKU-001",
        100,
        20,
        80,
        5)
};

        queries
            .Setup(x => x.GetReportAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(items);

        var handler = new GetInventoryReportQueryHandler(
            queries.Object);

        var query = new GetInventoryReportQuery();

        // Act
        var result = await handler.Handle(
            query,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Should().HaveCount(1);

        result.Value[0].SKU.Should().Be("SKU-001");
        result.Value[0].OnHandQuantity.Should().Be(100);
        result.Value[0].ReservedQuantity.Should().Be(20);
        result.Value[0].AvailableQuantity.Should().Be(80);
        result.Value[0].MinimumQuantity.Should().Be(5);

        queries.Verify(
            x => x.GetReportAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
    [Fact]
    public async Task Handle_Should_Return_Empty_List_When_No_Inventory_Items_Exist()
    {
        // Arrange
        var queries = new Mock<IInventoryQueries>();

        queries
            .Setup(x => x.GetReportAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var handler = new GetInventoryReportQueryHandler(
            queries.Object);

        var query = new GetInventoryReportQuery();

        // Act
        var result = await handler.Handle(
            query,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();

        queries.Verify(
            x => x.GetReportAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
