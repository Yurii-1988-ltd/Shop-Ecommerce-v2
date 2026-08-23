using Ecommerce.Cart.Modules.Domain.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

public class CustomCouponCodeSerializer : SerializerBase<CouponCode>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, CouponCode value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else
        {
            context.Writer.WriteString(value.Value); // Записываем строку ECOMMERCE-#123-456-78
        }
    }

    public override CouponCode Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var codeString = context.Reader.ReadString();
        return CouponCode.Create(codeString).Value; // Восстанавливаем Value Object
    }
}