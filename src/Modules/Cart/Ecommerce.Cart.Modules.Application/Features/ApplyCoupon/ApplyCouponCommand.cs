using Ecommerce.Application.CQRS;

namespace Ecommerce.Cart.Modules.Application.Features.ApplyCoupon;

public sealed record ApplyCouponCommand(Guid CustomerId, string Code): ICommand;

