using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Entities;

public sealed class ProductImage : Entity
{
    //public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    /// <summary>
    /// Relative path or storage key.
    /// Example: products/2026/07/iphone16-front.jpg
    /// </summary>
    public string StorageKey { get; private set; } = string.Empty;

    public string AltText { get; private set; } = string.Empty;

    public bool IsPrimary { get; private set; }

    public int DisplayOrder { get; private set; }

    public Guid? ProductVariantId { get; private set; }

    private ProductImage()
    {
    }

    public static Result<ProductImage> Create(
        Guid productId,
        string storageKey,
        string? altText,
        bool isPrimary,
        int displayOrder,
        Guid? productVariantId = null)
    {
        if (productId == Guid.Empty)
            return ImagesErrors.InvalidProductId;

        if (string.IsNullOrWhiteSpace(storageKey))
            return ImagesErrors.UrlIsRequired;

        if (displayOrder < 0)
            return ImagesErrors.InvalidDisplayOrder;

        return new ProductImage
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            StorageKey = storageKey.Trim(),
            AltText = altText?.Trim() ?? string.Empty,
            IsPrimary = isPrimary,
            DisplayOrder = displayOrder,
            ProductVariantId = productVariantId
        };
    }

    public Result Update(
        string storageKey,
        string? altText,
        bool isPrimary,
        int displayOrder,
        Guid? productVariantId = null)
    {
        if (string.IsNullOrWhiteSpace(storageKey))
            return ImagesErrors.UrlIsRequired;

        if (displayOrder < 0)
            return ImagesErrors.InvalidDisplayOrder;

        StorageKey = storageKey.Trim();
        AltText = altText?.Trim() ?? string.Empty;
        IsPrimary = isPrimary;
        DisplayOrder = displayOrder;
        ProductVariantId = productVariantId;

        return Result.Success();
    }

    public void SetAsPrimary()
    {
        IsPrimary = true;
    }

    public void RemovePrimary()
    {
        IsPrimary = false;
    }

    public Result ChangeDisplayOrder(int displayOrder)
    {
        if (displayOrder < 0)
            return ImagesErrors.InvalidDisplayOrder;

        DisplayOrder = displayOrder;

        return Result.Success();
    }
}