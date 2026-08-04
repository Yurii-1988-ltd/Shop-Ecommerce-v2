namespace Ecommerce.Admin.ApiClients.Brands.Models;

public sealed class BrandListItemResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public bool IsActive { get; init; }
}
