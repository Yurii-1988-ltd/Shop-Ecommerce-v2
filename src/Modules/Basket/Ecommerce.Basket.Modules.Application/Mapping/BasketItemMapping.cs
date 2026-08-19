using Ecommerce.Basket.Modules.Domain.Entities;
using MongoDB.Bson.Serialization;

public static class BasketItemMapping
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(BasketItem)))
            return;

        BsonClassMap.RegisterClassMap<BasketItem>(cm =>
        {
            cm.AutoMap();
        });
    }
}