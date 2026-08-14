using Ecommerce.Modules.Users.Application.Abstractions;
using Ecommerce.Modules.Users.Contracts.Abstractions; // Обязательно импортируйте namespace контрактов
  // Укажите корректный namespace для ваших реализаций
using Ecommerce.Modules.Users.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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