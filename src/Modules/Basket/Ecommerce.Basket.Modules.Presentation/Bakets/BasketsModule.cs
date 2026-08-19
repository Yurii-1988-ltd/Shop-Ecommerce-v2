using Ecommerce.Basket.Modules.Infrastructure;
using Ecommerce.Basket.Modules.Presentation.Baskets;
using Ecommerce.Presentation;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Basket.Modules.Presentation.Bakets;

public class BasketsModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
      new AddItemToBasketEndpoint().MapEndpoint(app);
      new GetBasketEndpoint().MapEndpoint(app);
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddBasketModule(config);
    }
}
