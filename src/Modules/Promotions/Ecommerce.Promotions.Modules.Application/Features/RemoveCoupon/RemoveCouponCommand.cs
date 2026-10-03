

using Ecommerce.Application.CQRS;

namespace Ecommerce.Promotions.Modules.Application.RemoveCoupon;

public sealed record RemoveCouponCommand(Guid CustomerId) : ICommand;

