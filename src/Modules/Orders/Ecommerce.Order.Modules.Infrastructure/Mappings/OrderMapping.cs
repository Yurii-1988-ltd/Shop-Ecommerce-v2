using Ecommerce.Order.Modules.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Ecommerce.Order.Modules.Infrastructure.Data.Mappings;

public static class OrderMapping
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Ecommerce.Order.Modules.Domain.Entities.Order)))
            return;

        BsonClassMap.RegisterClassMap<Ecommerce.Order.Modules.Domain.Entities.Order>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);

            // 1. Привязываем приватное поле _items к документу BSON как "Items"
            cm.MapField("_items")
              .SetElementName("Items");

            // 2. Игнорируем свойства, чтобы не было конфликта имён элементов в BSON
            cm.UnmapProperty(x => x.Items);
            cm.UnmapProperty(x => x.TotalQuantity);
        });
    }
}