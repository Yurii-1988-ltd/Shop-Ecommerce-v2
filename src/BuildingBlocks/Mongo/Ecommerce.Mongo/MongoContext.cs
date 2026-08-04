using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Ecommerce.Mongo;

public sealed class MongoContext : IMongoContext
{
    public IMongoDatabase Database { get; }

    public MongoContext(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"]
                               ?? throw new InvalidOperationException("MongoDb:ConnectionString not found");

        var databaseName = configuration["MongoDb:Database"]
                           ?? throw new InvalidOperationException("MongoDb:Database not found");

        var client = new MongoClient(connectionString);

        Database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
        => Database.GetCollection<T>(name);
}