using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Mongo;

public static class MongoRegistration
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services.AddSingleton<IMongoContext, MongoContext>();

        return services;
    }
}