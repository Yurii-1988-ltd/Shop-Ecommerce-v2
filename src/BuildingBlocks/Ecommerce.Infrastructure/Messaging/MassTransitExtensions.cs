

using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Infrastructure.Messaging;

public static class MassTransitExtensions
{
    public static IServiceCollection AddCustomTransit(this IServiceCollection services,
                    IConfiguration configuration,params Assembly[]assemblies)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(assemblies);
            x.UsingRabbitMq((context, cfg) =>
            {
                var connectionString = configuration.GetConnectionString("messaging")
                ?? throw new InvalidOperationException("Connection string 'messaging' for RabbitMQ is missing.");
                cfg.Host(connectionString);
                cfg.UseMessageRetry(x => x.Interval(3, TimeSpan.FromSeconds(5)));
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
