
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddSortable();
        builder.Services.AddMudServices();

        builder.Services.AddHttpClient<ICategoryApiClient, CategoryApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<ICatalogApiClient, CatalogApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<IBrandApiClient, BrandsApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<IImageApiClient, ImageApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<ICartApiClient, CartApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<IOrderApiClient, OrderApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<IInventoryApiClient, InventoryApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<IUserApiClient, UserApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<IRoleApiClient, RoleApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<ICouponApiClient, CouponApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.AddHttpClient<IIdentityApiClient, IdentityApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125");
        });
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(
                new JsonStringEnumConverter(allowIntegerValues: true)
            );
            // Дозволяє співставляти "canceled", "Canceled", "CANCELED"
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
        });

        //Services
        builder.Services.AddScoped<ITokenStorage, TokenStorage>();
        builder.Services.AddScoped<AuthenticationService>();
        //builder.AddServiceDefaults();
        var app = builder.Build();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();


        app.Run();
    }
}