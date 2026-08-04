
using MongoDB.Bson.Serialization;


namespace Ecommerce.Catalog.Modules.Infrastructure.Mappings;

//internal sealed class CategoryMapping
//{
//    public static void Register()
//    {
//        if (BsonClassMap.IsClassMapRegistered(typeof(Domain.Entities.Category)))
//        {
//            return;
//        }

//        BsonClassMap.RegisterClassMap<Domain.Entities.Category>(cm =>
//        {
//            cm.AutoMap();

//            cm.MapIdMember(x => x.Id);

//            cm.SetIgnoreExtraElements(true);
//        });
//    }
//}
