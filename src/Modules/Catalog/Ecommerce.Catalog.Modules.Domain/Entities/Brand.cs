
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Entities;

public sealed class Brand: Entity
{

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }

    private Brand()
    {
    }
    public static Result<Brand> Create(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BrandErrors.NameIsRequired;
        return new Brand
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };
    }
    public Result Update(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return CategoryErrors.NameIsRequired;
        Name = name;
        Description = description;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }
}
