using Ecommerce.Localization.Modules.Infrastructure.Services;

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
                "Connection string 'localizations' is not configured.");

     
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();

        services.AddSingleton(dataSource);

        // 2. EF Core
        services.AddDbContext<LocalizationContext>(options =>
        {
            options.UseNpgsql(dataSource);
        });

        // 3. MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(CreateTranslationCommandHandler).Assembly));

        // 4. Repositories
        services.AddScoped<ITranslationRepository, TranslationRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();

        // 5. Dapper queries
        services.AddScoped<ILocalizationQueries, LocalizationQueries>();
        services.AddScoped<ILanguageQueries, LanguageQueries>();

        // 6. Services
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<ILocalizationUnitOfWork, LocalizationUnitOfWork>();
        services.AddScoped<ICurrentCulture, CurrentCulture>();

        return services;
    }

    public static async Task ApplyLocalizationMigrationsAsync(this IHost host)
    {
        await using var scope = host.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<LocalizationContext>();

        await context.Database.MigrateAsync();
        await LocalizationSeeder.SeedAsync(context);
    }
}