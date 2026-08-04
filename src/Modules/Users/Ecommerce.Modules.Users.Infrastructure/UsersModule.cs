
namespace Ecommerce.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        return services;

    }
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    { 
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddDbContext<UserDbContext>((sp, options) => {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
            options.AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>());
        });

        // Register Repositories
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();


        // Register Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
        services.AddScoped<IUserQueries, UserQueries>();
        //Health Checks
        services.AddDefaultHealthChecks()
     .AddDbContextCheck<UserDbContext>("users-db");


       
    }

}
