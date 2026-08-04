using Ecommerce.API.ExceptionHandling;
using Ecommerce.API.Extensions;
using Ecommerce.Application.Behaviours;
using Ecommerce.Cart.Modules.Presentation.Carts;
using Ecommerce.Catalog.Modules.Infrastructure;
using Ecommerce.Catalog.Modules.Presentation.Products;
using Ecommerce.Mongo;
using Ecommerce.Order.Modules.Presentation.Orders;
using Ecommerce.ServiceDefaults;
using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Жестко привязываем WebRootPath для Aspire / Docker до сборки приложения
        builder.Environment.WebRootPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");

        builder.AddServiceDefaults<GlobalExceptionHandler>();
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddAntiforgery();
        builder.Services.AddCors();
        builder.Services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        builder.Services
            .AddModule<UsersModule>(builder.Configuration)
            .AddModule<IdentityModule>(builder.Configuration)
            .AddModule<ProductsModule>(builder.Configuration)
            .AddModule<CartsModule>(builder.Configuration)
            .AddModule<OrdersModule>(builder.Configuration);

        //Mongo
        builder.Services.AddSingleton<IMongoContext, MongoContext>();


        builder.Services.ConfigureHttpJsonOptions(options =>
        {
         
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
        });

        var app = builder.Build();
        app.UseCors(policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


        app.UseExceptionHandler();
        app.UseHttpsRedirection();


        app.UseStaticFiles();





        ProductModuleExtensions.UseWebApplicationExtensions(app);
        app.UseSwaggerDocumentation();

        app.UseAuthentication();
        app.UseAuthorization();

        // 4. Маппинг конечных точек
        app.MapDefaultEndpoints();
        app.MapModules();

        app.Run();
    }
}