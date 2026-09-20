

namespace Ecommerce.Notifications.Modules.Infrastructure;

public static class NotificationsModuleExtensions
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services, IConfiguration config)
    {
        // 1. MediatR (Application)
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(IEmailSender).Assembly));

        // 2. PostgreSQL + EF Core Context
        var connectionString = config.GetConnectionString("notifications")
            ?? throw new InvalidOperationException("Сonnection string 'notifications' is not found.");

        services.AddNpgsqlDataSource(connectionString);

        services.AddDbContext<NotificationsContext>((sp, options) =>
        {
            var dataSource = sp.GetRequiredService<NpgsqlDataSource>();
            options.UseNpgsql(dataSource);
        });

        // 3. Сервисы и Репозитории
        services.AddHttpClient<IEmailSender, EmailSender>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        return services;
    }
}