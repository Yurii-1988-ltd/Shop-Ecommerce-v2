

using Ecommerce.Catalog.Modules.Application.Features.Products.CreateProduct;
using Ecommerce.Catalog.Modules.Domain.Repositories;
using FluentAssertions;
using Moq;
using System.Timers;
using Ecommerce.Catalog.Modules.Domain.Entities;
using Xunit;

namespace Ecommerce.Catalog.Modules.Application.Tests;

public class CreateProductCommandHandlerTest
{
    [Fact]
    public async Task Handle_Should_Create_Product()
    {
        // Arrange
        var repository = new Mock<IProductRepository>();

        var handler = new CreateCatalogCommandHandler(repository.Object);

        var command = new CreateCatalogCommand(
            "iPhone 16",
            "PRD-2026-004588",
            "Apple phone",
            "IPH16",
            1000m,
            "USD");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        repository.Verify(
            x => x.InsertAsync(
                It.IsAny<Product>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
