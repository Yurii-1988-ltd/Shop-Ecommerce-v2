using Ecommerce.Cart.Modules.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Ecommerce.Cart.Modules.Application.Mapping;

public static class CouponMapping
{
    public static void Register()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(Coupon)))
        {
            BsonClassMap.RegisterClassMap<Coupon>(cm =>
            {
                cm.AutoMap();

                cm.SetIsRootClass(true);

                cm.MapMember(c => c.Code)
                    .SetSerializer(new CustomCouponCodeSerializer());
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(FixedAmountCoupon)))
        {
            BsonClassMap.RegisterClassMap<FixedAmountCoupon>(cm =>
            {
                cm.AutoMap();
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(PercentageCoupon)))
        {
            BsonClassMap.RegisterClassMap<PercentageCoupon>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}