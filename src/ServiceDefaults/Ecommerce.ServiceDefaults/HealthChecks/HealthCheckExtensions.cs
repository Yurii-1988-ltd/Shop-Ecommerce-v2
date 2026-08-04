

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.ServiceDefaults.HealthChecks;

public static class HealthCheckExtensions
{
    public static IHealthChecksBuilder AddDefaultHealthChecks(
       this IServiceCollection services)
    {
        return services.AddHealthChecks();
    }
}