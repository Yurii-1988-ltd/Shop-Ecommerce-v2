
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
        new AssignRoleEndpoint().MapEndpoint(app);
        new GetUserRolesendpoint().MapEndpoint(app);
        new RemoveUserRoleEndpoint().MapEndpoint(app);
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddUsersModule(config); // <-- Добавлена буква 's'
    }
}