

using Ecommerce.Application.Abstractions;
using Ecommerce.Inventory.Modules.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Inventory.Modules.Infrastructure;

public static class InventoryModuleExtensions
{

    public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddApplication()
            .AddInfrastructure(config);
        return services;
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {

        //Database
        services.AddDbContext<InventoryContext>(options =>
        {
           options.UseNpgsql(config.GetConnectionString("inventory"));
        });
        //Unit of Work
        services.AddScoped<IUnitOfWork>(u => u.GetRequiredService<InventoryContext>());

        //Repositories
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        return services;
    }
}
