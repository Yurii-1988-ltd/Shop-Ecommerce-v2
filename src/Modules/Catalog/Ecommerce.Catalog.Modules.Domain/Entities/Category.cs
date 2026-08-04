

using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Entities;

public sealed class Category : Entity
{
    #region Fields and constructors

    public string Name { get; set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsActive { get;private set; }
    public DateTime CreatedAtUtc { get;private set; }
    public DateTime? UpdateAtUtc  { get;private set; }

    private Category() { }
    #endregion
    // static factory

    public static Result<Category>Create(string name, string?description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return CategoryErrors.NameIsRequired;
        return new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };      
    }
    public Result Update(string name, string?description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return CategoryErrors.NameIsRequired;
        Name = name;
        Description = description;
        UpdateAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

}
