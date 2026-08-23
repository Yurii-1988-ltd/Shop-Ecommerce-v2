using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;


namespace Ecommerce.Presentation;

    public interface IModule
    {
        void RegisterServices(IServiceCollection services, IConfiguration config);
        void MapEndpoints(IEndpointRouteBuilder app);
    }

