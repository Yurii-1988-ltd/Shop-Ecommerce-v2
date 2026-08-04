using Ecommerce.Presentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ModuleRegistration
{
    public static IServiceCollection AddModule<TModule>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TModule : class, IModule, new()
    {
        var module = new TModule();

        module.RegisterServices(services, configuration);

        services.AddSingleton<IModule>(module);

        return services;
    }

    public static WebApplication MapModules(this WebApplication app)
    {
        foreach (var module in app.Services.GetServices<IModule>())
        {
            module.MapEndpoints(app);
        }

        return app;
    }
}