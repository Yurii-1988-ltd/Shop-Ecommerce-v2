



namespace Ecommerce.Promotions.Modules.Presentation.Promotions;

public sealed class PromotionsModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new GetCouponsEndpoint().MapEndpoint(app);
        new CreateCouponEndpoint().MapEndpoint(app);
        new UpdateCouponEndpoint().MapEndpoint(app);
        new DeleteCouponEndpoint().MapEndpoint(app);
        new GetCouponEndpoint().MapEndpoint(app);

    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddPromotionModule(config);
    }
}
