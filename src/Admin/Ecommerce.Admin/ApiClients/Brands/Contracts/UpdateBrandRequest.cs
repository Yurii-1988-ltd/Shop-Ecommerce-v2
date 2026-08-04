namespace Ecommerce.Admin.ApiClients.Brands.Contracts;



public sealed class UpdateBrandRequest
{
    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }
}