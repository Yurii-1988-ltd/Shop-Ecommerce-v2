


using MongoDB.Driver;

namespace Ecommerce.Order.Modules.Infrastructure.Database;

public interface IOrderDatabase
{
    IMongoCollection<Domain.Entities.Order>Orders { get;  }
}
