

using MongoDB.Driver;

namespace Ecommerce.Basket.Modules.Application.Abstractions.Data;

public interface IBasketDatabase
{
    IMongoCollection<Domain.Entities.Basket> Baskets { get; }
}
