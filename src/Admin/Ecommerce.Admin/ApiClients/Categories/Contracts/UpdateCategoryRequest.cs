namespace Ecommerce.Admin.ApiClients.Categories.Contracts;



public sealed class UpdateCategoryRequest
{
    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }
}