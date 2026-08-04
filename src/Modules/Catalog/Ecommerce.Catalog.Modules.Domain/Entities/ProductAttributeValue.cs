using Ecommerce.Catalog.Modules.Application.Entities;

public class ProductAttributeValue
{
    public Guid Id { get; private set; }

    // Связь с товаром
    public Guid ProductId { get; private set; }

    // Связь с самим атрибутом
    public Guid AttributeId { get; private set; }
    public ProductAttribute Attribute { get; private set; } = null!;

    // Конкретное значение для этого товара
    public string Value { get; private set; } = string.Empty; // Например: "8 ГБ", "Красный"

    private ProductAttributeValue() { }

    public static ProductAttributeValue Create(Guid attributeId, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Значение атрибута не может быть пустым.");

        return new ProductAttributeValue
        {
            Id = Guid.NewGuid(),
            AttributeId = attributeId,
            Value = value
        };
    }
}