
using Ecommerce.Modules.Users.Infrastructure;
using Ecommerce.Modules.Users.Presentation.EndPoints.Users;
using Ecommerce.Presentation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public sealed class UsersModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new GetUsersEndPoint().MapEndpoints(app);
        new GetUserEndpoint().MapEndpoints(app);
        new CreateUserEndpoint().MapEndpoints(app);
        new RemoveUserEndPoint().MapEndpoints(app);
        new UpdateUserEndPoint().MapEndpoints(app);

    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddUserModule(config);
    }
}