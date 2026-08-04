

using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Catalog.Modules.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace Ecommerce.Catalog.Modules.Domain.Tests;

public class CategoryTests
{
    [Fact]
    public void Create_Should_Return_Failure_When_Name_Is_Empty()
    {
        // Arrange
        var name = string.Empty;
        var description = "Description of Category";

        // Act
        var result = Category.Create(name, description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CategoryErrors.NameIsRequired);
    }
  
}
