
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
        new RefreshTokenEndpoint().MapEndpoints(auth);
        //new ResetPasswordEndpoint().MapEndpoints(auth);
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityModule(config);
    }
}