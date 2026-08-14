// Укажите корректный namespace для ваших реализаций
namespace Ecommerce.Modules.Users.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplication();
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Database")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository,UserRoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            // --- НЕДОСТАЮЩИЕ СЕРВИСЫ ДЛЯ ИСПРАВЛЕНИЯ ОШИБКИ ---
            services.AddScoped<IUserService, UserService>();   // Исправляет ошибки в ResetPassword, Register, Login, ForgotPassword
            services.AddScoped<IUserQueries, UserQueries>();   // Исправляет ошибки в GetOrder, GetOrders
            // --------------------------------------------------

            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}