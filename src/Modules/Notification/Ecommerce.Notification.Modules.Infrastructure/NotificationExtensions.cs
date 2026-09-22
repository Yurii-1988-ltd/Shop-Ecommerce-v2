using Ecommerce.Infrastructure.Messaging;
using Ecommerce.Notification.Modules.Application.Services;
using System.Net.Mail;


namespace Ecommerce.Notifications.Modules.Infrastructure;

public static class NotificationsModuleExtensions
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services, IConfiguration config)
    {
        // 1. MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(IEmailSender).Assembly));

        // 2. PostgreSQL
        var connectionString = config.GetConnectionString("notifications")
            ?? throw new InvalidOperationException("Connection string 'notifications' is not found.");

        services.AddNpgsqlDataSource(connectionString);

        services.AddDbContext<NotificationsContext>((sp, options) =>
        {
            var dataSource = sp.GetRequiredService<NpgsqlDataSource>();
            options.UseNpgsql(dataSource);
        });

        // 3. MassTransit
        services.AddCustomTransit(config, typeof(NotificationsModuleExtensions).Assembly);
        // 3. SMTP-клиент
        var smtpHost = config["Services:mailpit:smtp:0"] ?? "localhost";
        var smtpPort = int.TryParse(config["Services:mailpit:smtp:1"], out var port) ? port : 1025;

        // 4. Регистрация FluentEmail + Liquid Template Engine
        services
            .AddFluentEmail("noreply@ecommerce.com", "Ecommerce Store")
            .AddLiquidRenderer()
            .AddSmtpSender(new SmtpClient(smtpHost, smtpPort));

        // 4. Сервисы модуля
        services.AddScoped<IEmailSender, FluentEmailSender>(); // 
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        return services;
    }
}