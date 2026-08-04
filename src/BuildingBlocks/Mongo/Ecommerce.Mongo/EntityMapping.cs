using Ecommerce.Domain.Domain;
using MongoDB.Bson.Serialization;

public static class EntityMapping
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Entity)))
            return;

        BsonClassMap.RegisterClassMap<Entity>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(x => x.Id);

            // Ignore Domain Events when persisting to MongoDB
            cm.UnmapProperty(x => x.DomainEvents);

            cm.SetIgnoreExtraElements(true);
        });
    }
}