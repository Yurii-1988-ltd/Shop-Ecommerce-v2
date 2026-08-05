



namespace Ecommerce.Order.Modules.Presentation.Orders;

public sealed class OrdersModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new GetOrdersEndpoint().MapEndpoint(app);
        new GetOrderEndpoint().MapEndpoint(app);
        new CreateOrderEndpoint().MapEndpoint(app);
        new AddOrderItemEndpoint().MapEndpoint(app);
        new ChangeOrderStatusEndpoint().MapEndpoint(app);
        new CancelOrderEndpoint().MapEndpoint(app);
        new UpdateShippingAddressEndpoint().MapEndpoint(app);
        new SubmitOrderEndpoint().MapEndpoint(app);
        new ChangeOrderItemQuantityEndpoint().MapEndpoint(app);
        new ForceChangeOrderStatusEndpoint().MapEndpoint(app);
        new RemoveOrderItemEndpoint().MapEndpoint(app);
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddOrderModule(config);
    }
}
