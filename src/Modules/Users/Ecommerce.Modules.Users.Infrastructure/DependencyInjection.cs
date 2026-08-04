


using Ecommerce.Modules.Users.Application.Abstractions;
using Ecommerce.Modules.Users.Infrastructure.Services;

namespace Ecommerce.Modules.Users.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Database")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
