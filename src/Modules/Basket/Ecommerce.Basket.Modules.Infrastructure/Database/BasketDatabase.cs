

using Ecommerce.Basket.Modules.Application.Abstractions.Data;
using Ecommerce.Mongo;
using MongoDB.Driver;

namespace Ecommerce.Basket.Modules.Infrastructure.Database;

internal sealed class BasketDatabase : IBasketDatabase
{
    public IMongoCollection<Domain.Entities.Basket> Baskets {  get; }

    public BasketDatabase(IMongoContext context)
    {
        Baskets = context.GetCollection<Domain.Entities.Basket>("Baskets");


    }
}
