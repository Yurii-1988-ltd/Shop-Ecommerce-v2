

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

public sealed class InventoriesModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new CancelReservationEndpoint().MapEndpoint(app);
        new CommitReservationEndpoint().MapEndpoint(app);
        new DeductStockEndpoint().MapEndpoint(app);
        new ReplenishStockEndpoint().MapEndpoint(app);
        new ReserveStockEndpoint().MapEndpoint(app);
        new GetInventoryReportEndpoint().MapEndpoint(app);
        new CreateInventoryItemEndpoint().MapEndpoint(app);
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddInventoryModule(config);
    }
}
