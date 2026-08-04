using Ecommerce.Modules.Users.Application.Fiatures.CreateUser;
using System.Reflection;

namespace Ecommerce.API.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);

            });
            return services;
        }
    }
}
