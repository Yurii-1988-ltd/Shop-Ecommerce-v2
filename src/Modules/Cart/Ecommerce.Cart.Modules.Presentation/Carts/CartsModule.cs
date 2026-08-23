
namespace Ecommerce.Cart.Modules.Presentation.Carts;

public sealed class CartsModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCartModule(configuration);
       
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new CreateCartEndpoint().MapEndpoint(app);
        new GetCartsEndpoint().MapEndpoint(app);
        new ClearCartEndpoint().MapEndpoint(app);
        new AddCartItemEndpoint().MapEndpoint(app);
        new GetCartEndpoint().MapEndpoint(app);
        new RemoveCartItemEndpoint().MapEndpoint(app);
        new ChangeCartItemQuantityEndpoint().MapEndpoint(app);
        new RemoveCartEndpoint().MapEndpoint(app);
        new ApplyCouponEndpoint().MapEndpoint(app);
        new RemoveCouponEndpoint().MapEndpoint(app);
      //  new CreateCouponEndpoint().MapEndpoint(app);
    }
}