


using Ecommerce.Catalog.Modules.Presentation.Images;

namespace Ecommerce.Catalog.Modules.Presentation.Products;

public sealed class ProductsModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
       services.AddCatalogModule(config);
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new CreateProductEndpoint().MapEndpoints(app);
        new GetProductsEndpoint().MapEndpoints(app);
        new GetProductDetailsEndpoint().MapEndpoints(app);
        new GetProductEndpoint().MapEndpoints(app);
        new UpdateProductEndpoint().MapEndpoints(app);
        new RemoveProductEndpoint().MapEndpoints(app);



        // Categories endpoints
        new GetCategoryEndpoint().MapEndpoints(app);
        new GetCategoriesEndpoint().MapEndpoints(app);
        new CreateCategoryEndpoint().MapEndpoints(app);
        new UpdateCategoryEndpoint().MapEndpoints(app);
        new RemoveCategoryEndpoint().MapEndpoints(app);

        //brand endpoints
        new CreateBrandEndpoint().MapEndpoints(app);
        new GetBrandsEndpoint().MapEndpoints(app);
        new GetBrandEndpoint().MapEndpoints(app);
        new UpdateBrandEndpoint().MapEndpoints(app);
        new RemoveBrandEndpoint().MapEndpoints(app);
        
        //image endpoint
        new RemoveImageEndpoint().MapEndpoints(app);
        new ChangeImageOrderEndpoint().MapEndpoints(app);
        new SetPrimaryImageEndpoint().MapEndpoints(app);
        new AddProductImageEndpoint().MapEndpoints(app);
        new UploadImageEndpoint().MapEndpoints(app);
    
       
    }
}