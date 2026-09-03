using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Catalog.Modules.Application.Abstractions.Data;
using Ecommerce.Catalog.Modules.Application.Mapping;
using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Domain.Domain;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProducts;

internal sealed class GetProductsQueryHandler(
    ICatalogDatabase context)
    : IQueryHandler<GetProductsQuery, PagedResult<ProductListItemResponse>>
{
    public async Task<Result<PagedResult<ProductListItemResponse>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Empty;
        if(!string.IsNullOrWhiteSpace(request.Search))
        {
            var search =request.Search.Trim();
            filter = Builders<Product>.Filter.Or(Builders<Product>.Filter.Regex(
                x=>x.Name,
                new MongoDB.Bson.BsonRegularExpression(search, "i")),
                Builders<Product>.Filter.Regex(
                    x => x.Sku,
                    new MongoDB.Bson.BsonRegularExpression(search, "i")),
                Builders<Product>.Filter.Regex(
                    x => x.ProductNumber,
                    new MongoDB.Bson.BsonRegularExpression(search, "i")));
        }

        var totalCount = (int)await context.Products
            .CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var products = await context.Products
            .Find(filter)
            .Skip((request.Page - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);




        var items = products
            .Select(x => x.ToListItemResponse())
            .ToList();

        return Result<PagedResult<ProductListItemResponse>>.Success(
     new PagedResult<ProductListItemResponse>
     {
         Items = items,
         Page = request.Page,
         PageSize = request.PageSize,
         TotalCount = totalCount
     });
    }
}