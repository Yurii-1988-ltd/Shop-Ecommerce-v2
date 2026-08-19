using Ecommerce.Basket.Modules.Domain.Entities;
using MongoDB.Bson.Serialization;

public static class BasketMapping
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Basket)))
            return;

        BsonClassMap.RegisterClassMap<Basket>(cm =>
        {
            cm.AutoMap();

            cm.MapField("_items")
                .SetElementName("Items");

            cm.UnmapProperty(b => b.Items);
            cm.UnmapProperty(b => b.RawSubtotal);
            cm.UnmapProperty(b => b.DiscountTotal);
            cm.UnmapProperty(b => b.GrandTotal);
            cm.UnmapProperty(b => b.Currency);
        });
    }
}