

namespace Ecommerce.Catalog.Modules.Application.Entities;

public sealed class ProductAttribute
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
}
