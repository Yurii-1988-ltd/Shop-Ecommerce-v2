

namespace Ecommerce.Cart.Modules.Infrastructure;

public static class CartModuleExtensions
{
    public static IServiceCollection AddCartModule(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMongo(configuration);
        services.AddApplication();
        services.AddInfrastructure();
 
        return services;

    }

    public static IServiceCollection AddMongo(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        CartMapping.Register();
        MongoMappings.Register();
        services.AddMongo();
     

        return services;
    }

    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateCartCommand).Assembly));

        return services;
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICartRepository, CartRepository>();
        return services;
    }
    public static WebApplication UseWebApplicationExtensions(this WebApplication app)
    {
        app.UseAntiforgery();
        return app;

    }
}
