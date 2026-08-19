using Ecommerce.Basket.Modules.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Ecommerce.Basket.Modules.Infrastructure.Mongo.Serializers;

public sealed class BasketQuantitySerializer : SerializerBase<BasketQuantity>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BasketQuantity value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
            return;
        }

        // Всегда сохраняем как простое число Int32
        context.Writer.WriteInt32(value.Value);
    }

    public override BasketQuantity Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonType = context.Reader.CurrentBsonType;

        int intValue = 0;

        if (bsonType == BsonType.Int32)
        {
            // Новый чистый формат: "Quantity": 2
            intValue = context.Reader.ReadInt32();
        }
        else if (bsonType == BsonType.Document)
        {
            // Старый формат BSON-документа: "Quantity": { "Value": 2 }
            context.Reader.ReadStartDocument();

            // Безопасно обходим элементы документа, проверяя состояние
            while (context.Reader.ReadBsonType() != BsonType.EndOfDocument)
            {
                var name = context.Reader.ReadName();
                if (name == "Value" || name == "value")
                {
                    intValue = context.Reader.ReadInt32();
                }
                else
                {
                    context.Reader.SkipValue();
                }
            }

            context.Reader.ReadEndDocument();
        }
        else if (bsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null!;
        }
        else
        {
            throw new BsonSerializationException($"Cannot deserialize BasketQuantity from BsonType {bsonType}");
        }

        var result = BasketQuantity.Create(intValue);
        if (result.IsFailure)
        {
            throw new BsonSerializationException(result.Error.Description);
        }

        return result.Value;
    }
}