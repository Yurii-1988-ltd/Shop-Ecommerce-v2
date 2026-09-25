
using Ecommerce.Notification.Modules.Infrastructure.Configuration;
using Ecommerce.Notification.Modules.Infrastructure.Adapters;

namespace Ecommerce.Notification.Modules.Infrastructure;

public static class NotificationsModuleExtensions
{
    public static IServiceCollection AddNotificationsModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(INotificationService).Assembly));

        // PostgreSQL
        var connectionString =
            config.GetConnectionString("notifications")
            ?? throw new InvalidOperationException(
                "Connection string 'notifications' is not found.");

        services.AddNpgsqlDataSource(
            connectionString,
            serviceKey: "notification");

        services.AddDbContext<NotificationsContext>((sp, options) =>
        {
            var dataSource =
                sp.GetRequiredKeyedService<NpgsqlDataSource>(
                    "notification");

            options.UseNpgsql(dataSource);
        });

        // MassTransit
        services.AddCustomTransit(
            config,
            typeof(NotificationsModuleExtensions).Assembly);

        // SMTP
        var smtpOptions = config
            .GetSection("SmtpOptions")
            .Get<SmtpOptions>()
            ?? throw new InvalidOperationException(
                "SmtpOptions configuration is not configured.");

        Console.WriteLine(
            $"SMTP CONFIG => {smtpOptions.Host}:{smtpOptions.Port}");

        services
            .AddFluentEmail(
                smtpOptions.FromEmail,
                smtpOptions.FromName)
            .AddLiquidRenderer()
            .AddSmtpSender(
                smtpOptions.Host,
                smtpOptions.Port);

        // Services
        services.AddScoped<
            INotificationUnitOfWork,
            NotificationsUnitOfWork>();

        services.AddScoped<
            INotificationRepository,
            NotificationRepository>();

        services.AddScoped<
            IEmailSender,
            FluentEmailSender>();

        services.AddScoped<
            INotificationService,
            NotificationService>();

        return services;
    }

    public static async Task ApplyNotificationsMigrationsAsync(
        this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<NotificationsContext>();

        await context.Database.MigrateAsync();
    }
}