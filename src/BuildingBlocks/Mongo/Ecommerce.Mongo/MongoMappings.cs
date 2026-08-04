using Ecommerce.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

public static class MongoMappings
{
    private static bool _registered;

    public static void Register()
    {
        if (_registered)
            return;

        BsonSerializer.RegisterSerializer(
            new GuidSerializer(GuidRepresentation.Standard));

      //  EntityMapping.Register();
      

        _registered = true;
    }
}