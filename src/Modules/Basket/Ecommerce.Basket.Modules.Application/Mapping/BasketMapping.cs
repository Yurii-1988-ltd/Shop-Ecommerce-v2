using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Basket.Modules.Application.Mapping
{
    public static class BasketMapping
    {
        public static void Register()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(Domain.Entities.Basket)))
                return;
            BsonClassMap.RegisterClassMap<Domain.Entities.Basket>(b =>
            {
                b.AutoMap();
            });

        }
    }
}
