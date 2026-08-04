

using Microsoft.AspNetCore.Builder;

namespace Ecommerce.ServiceDefaults;

public static class DefaultEndpoints
{
    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health");
        return app;
    }
}
