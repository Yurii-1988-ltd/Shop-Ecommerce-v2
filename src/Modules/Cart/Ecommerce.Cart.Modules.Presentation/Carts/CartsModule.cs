using Ecommerce.Cart.Modules.Infrastructure;
using Ecommerce.Presentation;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Intrinsics.Arm;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

public sealed class CartsModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCartModule(configuration);
       
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new CreateCartEndpoint().MapEndpoints(app);
        new GetCartsEndpoint().MapEndpoints(app);
        new ClearCartEndpoint().MapEndpoints(app);
        new AddCartItemEndpoint().MapEndpoints(app);
        new GetCartEndpoint().MapEndpoints(app);
        new RemoveCartItemEndpoint().MapEndpoints(app);
        new ChangeCartItemQuantityEndpoint().MapEndpoints(app);
        new RemoveCartEndpoint().MapEndpoints(app);
    }
}