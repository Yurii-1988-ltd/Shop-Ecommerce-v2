
using Ecommerce.Storefront.ApiClients.Inventories;
using Ecommerce.Storefront.Endpoints.Identity;
using Ecommerce.Storefront.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ICatalogApiClient, CatalogApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7125");
});
builder.Services.AddHttpClient<ICartApiClient, CartApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7125");
});
builder.Services.AddHttpClient<IIdentityApiClient, IdentityApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7125");
});
builder.Services.AddHttpClient<IInventoryApiClient, InventoryApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7125");
});
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddStorefrontAuthentication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();





var app = builder.Build();
new LoginEndpoint().MapEndpoint(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);    
    app.UseHsts();
}
app.UseMiddleware<GuestCartMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
