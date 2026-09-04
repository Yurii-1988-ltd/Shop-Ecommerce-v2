using Ecommerce.Storefront.ApiClients.Cart;
using Ecommerce.Storefront.ApiClients.Carts;
using Ecommerce.Storefront.ApiClients.Catalogs;
using Ecommerce.Storefront.ApiClients.Identity;
using Ecommerce.Storefront.Components;
using Ecommerce.Storefront.Extensions;

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
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddStorefrontAuthentication();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);    
    app.UseHsts();
}
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
