using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Modules.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
       
        });

        services.AddValidatorsFromAssembly(typeof(CreateUserCommandHandler).Assembly);

      

        return services;
    }
    
    
}
