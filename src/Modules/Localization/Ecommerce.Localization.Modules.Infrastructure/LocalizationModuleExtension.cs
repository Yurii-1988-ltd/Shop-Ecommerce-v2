
using Ecommerce.Localization.Modules.Application.Features.Translation.CreateTranslation;
using Ecommerce.Localization.Modules.Infrastructure.Database.Queries;
using Microsoft.Extensions.Hosting;


namespace Ecommerce.Localization.Modules.Infrastructure;

public static class LocalizationModuleExtensions
{
    public static IServiceCollection AddLocalizationModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        var connectionString =
            config.GetConnectionString("localizations")
            ?? throw new InvalidOperationException(
                "Connection string 'localization' is not configured.");

        services.AddNpgsqlDataSource(
            connectionString,
            serviceKey: "localization");

        services.AddDbContext<LocalizationContext>(
            (sp, options) =>
            {
                var dataSource =
                    sp.GetRequiredKeyedService<NpgsqlDataSource>(
                        "localization");

                options.UseNpgsql(dataSource);
            });
        services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateTranslationCommandHandler).Assembly));

        services.AddScoped<
            ITranslationRepository,
            TranslationRepository>();
        services.AddScoped
            <ILocalizationQueries,
            LocalizationQueries>();

        services.AddScoped<
            ILocalizationService,
            LocalizationService>();
        services.AddScoped<ILocalizationUnitOfWork,
            LocalizationUnitOfWork>();

        return services;
    }

    public static async Task ApplyLocalizationMigrationsAsync(
        this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<LocalizationContext>();

        await context.Database.MigrateAsync();

        await LocalizationSeeder.SeedAsync(context);
    }
}