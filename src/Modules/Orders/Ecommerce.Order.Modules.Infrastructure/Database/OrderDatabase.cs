
using Ecommerce.Mongo;
using Ecommerce.Order.Modules.Domain.Entities;
using Ecommerce.Order.Modules.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver;

namespace Ecommerce.Order.Modules.Infrastructure.Database
{
    internal sealed class OrderDatabase : IOrderDatabase
    {
        public IMongoCollection<Domain.Entities.Order> Orders { get; }

        public OrderDatabase(IMongoContext context)
        {
            Orders = context.GetCollection<Domain.Entities.Order>("orders");
            
        }
    }
}
