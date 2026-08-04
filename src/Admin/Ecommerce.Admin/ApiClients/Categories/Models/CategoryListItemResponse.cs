namespace Ecommerce.Admin.ApiClients.Categories.Models;

public sealed class CategoryListItemResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public bool IsActive { get; init; }
}
