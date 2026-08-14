using Ecommerce.Modules.Users.Infrastructure;// Добавьте namespace для CreateRoleEndpoint, если он там
using Ecommerce.Modules.Users.Presentation.EndPoints.Users;
using Ecommerce.Presentation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public sealed class UsersModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new GetUsersEndpoint().MapEndpoints(app);
        new GetUserEndpoint().MapEndpoints(app);
        new CreateUserEndpoint().MapEndpoints(app);
        new RemoveUserEndpoint().MapEndpoints(app);
        new UpdateUserEndpoint().MapEndpoints(app);
        new CreateRoleEndpoint().MapEndpoint(app);
        new GetRolesEndpoint().MapEndpoint(app);
        new GetRoleEndpoint().MapEndpoint(app);
        new UpdateRoleEndpoint().MapEndpoints(app);
        new RemoveRoleEndpoint().MapEndpoints(app);
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddUsersModule(config); // <-- Добавлена буква 's'
    }
}