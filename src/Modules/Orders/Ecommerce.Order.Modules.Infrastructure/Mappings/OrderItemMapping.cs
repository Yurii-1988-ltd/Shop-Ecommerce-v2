

using Ecommerce.Order.Modules.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Ecommerce.Order.Modules.Infrastructure.Mappings;

public static class OrderItemMapping
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(OrderItem)))
            return;

        BsonClassMap.RegisterClassMap<OrderItem>(cm =>
        {
            cm.AutoMap();
        });
    }
}
