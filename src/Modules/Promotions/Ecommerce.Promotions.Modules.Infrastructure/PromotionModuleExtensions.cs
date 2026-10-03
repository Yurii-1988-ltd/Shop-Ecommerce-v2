
namespace Ecommerce.Promotions.Modules.Infrastructure;

public static class PromotionModuleExtensions
{
    public static IServiceCollection AddPromotionModule(this IServiceCollection services, IConfiguration configuration)
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
        MongoMappings.Register();

        CouponMapping.Register();

        services.AddMongo();


        return services;
    }

    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(CreateCouponCommandHandler).Assembly));

        return services;
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICouponService, CouponValidationService>();

        services.AddScoped<ICouponRepository, CouponRepository>();
        return services;
      
    }
    public static WebApplication UseWebApplicationExtensions(this WebApplication app)
    {
        app.UseAntiforgery();
        return app;

    }
}
