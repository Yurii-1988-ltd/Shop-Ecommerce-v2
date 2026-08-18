


using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Mappings;
using Ecommerce.Order.Modules.Infrastructure.Data.Mappings;
using Ecommerce.Order.Modules.Infrastructure.Services;

namespace Ecommerce.Order.Modules.Infrastructure;

public static class OrderModuleExtensions
{
   public static IServiceCollection AddOrderModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
        services.AddMongo(configuration);
        return services;

    }
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {

        MongoMappings.Register();

        MoneyMapping.Register();

        OrderItemMapping.Register();

        OrderMapping.Register();

        services.AddMongo(); ;
        return services;

    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GetOrdersQuery).Assembly));
        return services;

    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEntityNumberGenerator, OrderNumberGenerator>();
        services.AddScoped<IOrderDatabase, OrderDatabase>();

        //services
        services.AddScoped<IProductService, ProductService>();
   
        return services;

    }
}

