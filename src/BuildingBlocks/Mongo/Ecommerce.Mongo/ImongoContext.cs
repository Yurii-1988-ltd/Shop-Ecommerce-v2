

using MongoDB.Driver;

namespace Ecommerce.Mongo;

public interface IMongoContext
{
    IMongoDatabase Database { get; }

    IMongoCollection<T> GetCollection<T>(string name);
}
