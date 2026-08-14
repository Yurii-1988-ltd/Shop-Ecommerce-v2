using Ecommerce.Modules.Users.Application.Fiatures.CreateRole;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Modules.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(CreateUserCommandHandler).Assembly,
                typeof(CreateRoleCommandHandler).Assembly // Добавьте сборку, где лежит CreateRoleCommandHandler
            );
        });

        services.AddValidatorsFromAssembly(typeof(CreateUserCommandHandler).Assembly);

        return services;
    }


}
