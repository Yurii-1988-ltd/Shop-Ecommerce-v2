using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Catalog.Modules.Domain.Events;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Catalog.Modules.Domain.Entities;

public sealed class Product : Entity
{
    // Используем Guid для безопасности и простоты распределенных систем


    public string Name { get; private set; } = string.Empty;
    public string ProductNumber { get; private set; } = default!;
    public string Description { get; private set; } = string.Empty;
    public string Sku { get; private set; } = string.Empty; // Stock Keeping Unit (артикул)

    public Money Price { get; private set; }
    public Money? SalePrice { get; private set; } // Цена со скидкой (если есть)

    public int StockQuantity { get; private set; }

    public bool InStock => StockQuantity > 0;

    public bool OutOfStock => StockQuantity == 0;
    public bool IsActive { get; private set; }

    // Связи (Категория и Бренд)
    public Guid? CategoryId { get; private set; }
    public Guid? BrandId { get; private set; }

    // Аудит-свойства
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }

    // Навигационные свойства для EF Core / ORM
    public List<ProductImage> Images { get; private set; } = new();
    public List<ProductAttributeValue> AttributeValues { get; private set; } = new();


    private Product() { }

  
    public static Result<Product> Create(string name, string productNumber, string sku, Money price, string description = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            return ProductErrors.NameIsRequired;
         
        if (string.IsNullOrWhiteSpace(sku))
            return ProductErrors.SkuIsRequired;

        //if (price.Amount < 0)
        //    return ProductErrors.NagativePrice;

        var product =  new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            ProductNumber = productNumber,
            Sku = sku.ToUpperInvariant(), // Стандартизируем SKU
            Price = price,
            Description = description,
           // CategoryId = categoryId,
           // BrandId = brandId,
            StockQuantity = 0,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };
        product.AddDomainEvent(new ProductCreatedDomainEvent(product.Id));
        return product;
        
    }

    public Result UpdatePrice(Money newPrice, Money? newSalePrice = null)
    {
        if (newSalePrice is not null)
        {
            if (newSalePrice.Currency != newPrice.Currency)
            {
                return ProductErrors.CurrencyMismatch;
            }

            if (newSalePrice.Amount > newPrice.Amount)
            {
                return ProductErrors.InvalidSalePrice;
            }
        }

        Price = newPrice;
        SalePrice = newSalePrice;
        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result UpdateStock(int quantity)
    {
        if (quantity < 0)
            return ProductErrors.NegativeStockQuantity;

        StockQuantity = quantity;
        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }
    public Result UpdateInformation(
    string name,
    string description,
    string sku)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ProductErrors.NameIsRequired;

        if (string.IsNullOrWhiteSpace(sku))
            return ProductErrors.SkuIsRequired;

        Name = name;
        Description = description;
        Sku = sku.ToUpperInvariant();

        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result Archive()
    {
        if (!IsActive)
        {
            return ProductErrors.ProductAlreadyArchived;
        }

        IsActive = false;
        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }
    public Result AddImage(
        string storageKey,
        string? altText,
        Guid? productVariantId = null)
    {
        var imageResult = ProductImage.Create(
            productId: Id,
            storageKey: storageKey,
            altText: altText,
            isPrimary: Images.Count == 0,
            displayOrder: Images.Count,
            productVariantId: productVariantId);

        if (imageResult.IsFailure)
            return imageResult.Error;

        Images.Add(imageResult.Value);

        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }
    
    public Result SetPrimaryImage(Guid imageId)
    {
        var image = Images.FirstOrDefault(x => x.Id == imageId);

        if (image is null)
            return ImagesErrors.NotFound(imageId);

        if (image.IsPrimary)
            return Result.Success();

        foreach (var item in Images)
        {
            item.RemovePrimary();
        }

        image.SetAsPrimary();

        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }
    public Result RemoveImage(Guid imageId)
    {
        var image = Images.FirstOrDefault(x => x.Id == imageId);

        if (image is null)
            return ImagesErrors.NotFound(imageId);

        var wasPrimary = image.IsPrimary;

        Images.Remove(image);

        if (wasPrimary && Images.Count > 0)
        {
            Images
                .OrderBy(x => x.DisplayOrder)
                .First()
                .SetAsPrimary();
        }

        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result ReorderImages(Guid imageId, int newIndex)
    {
        var image = Images.FirstOrDefault(x => x.Id == imageId);
        if (image is null)
            return ImagesErrors.NotFound(imageId);
        var ordered = Images.OrderBy(x=>x.DisplayOrder).ToList();
        ordered.Remove(image);
        if (newIndex<0)
        {
            newIndex = 0;
        }
        ordered.Insert(newIndex, image);
        for (int i = 0; i < ordered.Count; i++)
        {
            ordered[i].ChangeDisplayOrder(i);
        }
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
        
    }
    
  

}