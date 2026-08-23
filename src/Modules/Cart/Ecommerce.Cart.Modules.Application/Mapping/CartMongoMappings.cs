using MongoDB.Bson.Serialization;

namespace Ecommerce.Cart.Modules.Application.Mapping;

public static class CartMapping
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Domain.Entities.Cart)))
            return;

        BsonClassMap.RegisterClassMap<Domain.Entities.Cart>(cm =>
        {
            cm.AutoMap();

            cm.MapField("_items")
                .SetElementName("Items");
        });
    }
}