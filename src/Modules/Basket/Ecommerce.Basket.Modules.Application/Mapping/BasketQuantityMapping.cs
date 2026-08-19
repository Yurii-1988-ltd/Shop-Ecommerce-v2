
using Ecommerce.Basket.Modules.Domain.ValueObjects;
using MongoDB.Bson.Serialization;
using Ecommerce.Basket.Modules.Infrastructure.Mongo.Serializers;
namespace Ecommerce.Basket.Modules.Application.Mapping;


public static class BasketQuantityMapping
{
    private static bool _registered;

    public static void Register()
    {
        if (_registered)
            return;

        BsonSerializer.RegisterSerializer(
            new BasketQuantitySerializer());

        _registered = true;
    }
}