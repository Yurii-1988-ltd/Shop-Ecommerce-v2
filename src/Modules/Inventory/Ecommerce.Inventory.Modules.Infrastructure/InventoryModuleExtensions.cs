


using Ecommerce.Inventory.Modules.Application.Features.GetInventoryReport;
using Ecommerce.Inventory.Modules.Infrastructure.Database.Queries;
using Export.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Npgsql;
namespace Ecommerce.Inventory.Modules.Infrastructure;

public static class InventoryModuleExtensions
{

    public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddApplication()
            .AddExportModule()
            .AddInfrastructure(config);
        return services;
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
           cfg.RegisterServicesFromAssembly(typeof(GetInventoryReportQuery).Assembly));
        return services;
    }
    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration config)
    {
        var connectionString = config.GetConnectionString("inventories")
            ?? throw new InvalidOperationException(
                "Inventory PostgreSQL connection string was not found.");

        services.AddNpgsqlDataSource(
            connectionString,
            serviceKey: "inventory");

        services.AddDbContext<InventoryContext>((sp, options) =>
        {
            var dataSource =
                sp.GetRequiredKeyedService<NpgsqlDataSource>("inventory");

            options.UseNpgsql(dataSource);
        });

        services.AddScoped<IInventoryUnitOfWork, InventoryUnitOfWork>();

        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IInventoryQueries, InventoryQueries>();

        return services;
    }
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<InventoryContext>();

        var connection = dbContext.Database.GetDbConnection();

 

        await dbContext.Database.MigrateAsync();
    }
}

