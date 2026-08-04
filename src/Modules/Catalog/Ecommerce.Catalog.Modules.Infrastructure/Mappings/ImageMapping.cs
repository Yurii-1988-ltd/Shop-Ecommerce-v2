using Ecommerce.Catalog.Modules.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Ecommerce.Catalog.Modules.Infrastructure.Mappings;

internal static class ImageMapping
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(ProductImage)))
            return;

        BsonClassMap.RegisterClassMap<ProductImage>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(x => x.Id);
            cm.SetIgnoreExtraElements(true);
        });
    }
}
