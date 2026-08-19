using Ecommerce.Basket.Modules.Application.Features.AddItemToBasket;
using Ecommerce.Basket.Modules.Application.Mapping;
using Ecommerce.Basket.Modules.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Basket.Modules.Infrastructure;


    public static class BaketModuleExtensions
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddApplication()
                .AddInfrastructure(configuration)
                .AddMongo(configuration);
            return services;
        }
        public static IServiceCollection AddMongo(this IServiceCollection services,IConfiguration configuration)
        {
            BasketMapping.Register();

            BasketQuantityMapping.Register();     
            BasketItemMapping.Register();
            MongoMappings.Register();
            services.AddMongo();
            return services;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(AddItemToBasketCommandHandler).Assembly));
            return services;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBasketRepository, MongoBasketRepository>();
            return services;
        }
    }

