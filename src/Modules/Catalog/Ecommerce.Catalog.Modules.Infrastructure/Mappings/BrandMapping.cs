

using Ecommerce.Catalog.Modules.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Ecommerce.Catalog.Modules.Infrastructure.Mappings;

//internal sealed class BrandMapping
//{
//    public static void Register()
//    {
//        if (BsonClassMap.IsClassMapRegistered(typeof(Brand)))
//            return;

//        BsonClassMap.RegisterClassMap<Brand>(cm =>
//        {
//            cm.AutoMap();

//            cm.MapIdMember(x => x.Id);

//            cm.SetIgnoreExtraElements(true);
//        });
//    }
//}
