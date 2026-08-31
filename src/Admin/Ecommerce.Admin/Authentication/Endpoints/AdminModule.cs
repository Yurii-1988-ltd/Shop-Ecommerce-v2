using Ecommerce.Presentation;

namespace Ecommerce.Admin.Authentication.Endpoints;

public class AdminModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new AdminLoginEndpoint().MapEndpoints(app);
        new AdminLogoutEndpoint().MapEndpoints(app);
     
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddAdminModule();
    }
}
