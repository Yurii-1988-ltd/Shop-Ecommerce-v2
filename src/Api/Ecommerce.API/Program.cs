
using Ecommerce.Inventory.Modules.Infrastructure;
using Export.Infrastructure;
using QuestPDF.Infrastructure;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        QuestPDF.Settings.License = LicenseType.Community;
        builder.Environment.WebRootPath =
            Path.Combine(builder.Environment.ContentRootPath, "wwwroot");

        builder.AddServiceDefaults<GlobalExceptionHandler>();

        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddAntiforgery();
        builder.Services.AddCors();

        builder.Services.AddExportModule();

        builder.Services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        builder.Services
            .AddModule<UsersModule>(builder.Configuration)
            .AddModule<IdentityModule>(builder.Configuration)
            .AddModule<ProductsModule>(builder.Configuration)
            .AddModule<CartsModule>(builder.Configuration)
            .AddModule<OrdersModule>(builder.Configuration)
            .AddModule<InventoriesModule>(builder.Configuration);
          


        builder.Services.AddSingleton<IMongoContext, MongoContext>();

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            options.SerializerOptions.PropertyNameCaseInsensitive = true;
        });

        var app = builder.Build();

        app.UseCors(policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        await app.MigrateDatabaseAsync();

        app.UseExceptionHandler();
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        ProductModuleExtensions.UseWebApplicationExtensions(app);

        app.UseSwaggerDocumentation();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapDefaultEndpoints();
        app.MapModules();

        await app.RunAsync();
    }
}