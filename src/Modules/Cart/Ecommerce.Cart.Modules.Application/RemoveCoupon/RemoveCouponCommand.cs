
using Ecommerce.Application.CQRS;

namespace Ecommerce.Cart.Modules.Application.RemoveCoupon;

public sealed record RemoveCouponCommand(Guid CustomerId) : ICommand;

