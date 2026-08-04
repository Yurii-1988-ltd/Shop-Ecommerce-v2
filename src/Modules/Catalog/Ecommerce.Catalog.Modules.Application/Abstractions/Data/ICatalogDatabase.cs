using Ecommerce.Catalog.Modules.Domain.Entities;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Modules.Application.Abstractions.Data;

public interface ICatalogDatabase 
{
    IMongoCollection<Product> Products { get; }
    IMongoCollection<Brand> Brands { get; }
    IMongoCollection<Category> Categories { get; }


}


