


using Ecommerce.Application.Mappings;
using Ecommerce.Catalog.Contracts.Products;
using Ecommerce.Catalog.Modules.Infrastructure.Services;
using Ecommerce.Mongo;
using Microsoft.AspNetCore.Builder;

namespace Ecommerce.Catalog.Modules.Infrastructure;

public static class ProductModuleExtensions
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMongo(configuration);
        services.AddApplication();
        return services;

    }
    public static IServiceCollection AddMongo(
     this IServiceCollection services,
     IConfiguration configuration)
    {
        MongoMappings.Register();
        MoneyMapping.Register();

        services.AddMongo();

        services.Configure<LocalStorageOptions>(
            configuration.GetSection(LocalStorageOptions.SectionName));

        services.Configure<FileStorageOptions>(
            configuration.GetSection(FileStorageOptions.SectionName));

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICatalogQueries, CatalogQueries>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddSingleton<ICatalogDatabase, CatalogDatabase>();
        services.AddScoped<IFileStorage, LocalFileStorage>();
        services.AddScoped<IFileValidator, FileValidator>();

        return services;
    }
    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCatalogCommand).Assembly));

        return services;
    }
    public static WebApplication UseWebApplicationExtensions(this WebApplication app)
    {
        app.UseAntiforgery();
        return app;

    }
}
