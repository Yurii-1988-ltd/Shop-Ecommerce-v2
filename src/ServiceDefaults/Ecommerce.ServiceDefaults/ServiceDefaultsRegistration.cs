using Ecommerce.ServiceDefaults.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceDefaultsRegistration
{
    public static WebApplicationBuilder AddServiceDefaults<TExceptionHandler>(
        this WebApplicationBuilder builder)
        where TExceptionHandler : class, IExceptionHandler
    {
        builder.Services.AddDefaultHealthChecks();

        builder.Services.AddProblemDetails();

        builder.Services.AddExceptionHandler<TExceptionHandler>();

        return builder;
    }
}