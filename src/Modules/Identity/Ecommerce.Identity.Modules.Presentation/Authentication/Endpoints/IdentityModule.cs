using Ecommerce.Identity.Modules.Infrastructure;

using Ecommerce.Identity.Modules.Presentation.Authentication.Endpoints;
using Ecommerce.Presentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public sealed class IdentityModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder auth = app
            .MapGroup("/auth")
            .WithTags("Authentication");

        new RegisterEndpoint().MapEndpoints(auth);
        new LoginEndpoint().MapEndpoints(auth);
        new ResetPasswordEndpoint().MapEndPoints(auth);
        new ForgotPasswordEndpoint().MapEndPoints(auth);
        //new ResetPasswordEndpoint().MapEndpoints(auth);
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityModule(config);
    }
}