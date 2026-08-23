using Ecommerce.Cart.Modules.Application.Contracts;
using Ecommerce.Cart.Modules.Application.Features.CreateCoupon;
using Ecommerce.Cart.Modules.Application.Features.Responses;
using Ecommerce.Cart.Modules.Domain.Entities;

internal static class CartMappings
{
    public static CartResponse ToResponse(this Cart cart)
    {
        var subTotal = cart.GetSubtotal().Value;
        var discount = cart.GetDiscountTotal().Value;
        var total = cart.GetTotalCost().Value;
   

        return new CartResponse(
            cart.Id,
            cart.CustomerId,
            cart.Items.Select(x => new CartItemResponse(
                    x.ProductId,
                    x.Name,
                    x.Price.Amount,
                    x.Price.Currency,
                    x.Quantity,
                    x.TotalPrice.Amount))
                .ToList(),
            cart.Items.Sum(x => x.Quantity),
            subTotal.Amount,
            discount.Amount,
            total.Amount,
            total.Currency);
    }

    public static CartListResponse ToListResponse(this Cart cart)
    {
        var total = cart.GetTotalCost().Value;

        return new CartListResponse(
            cart.Id,
            cart.CustomerId,
            cart.Items.Sum(x => x.Quantity),
            total.Amount,
            total.Currency);
    }
    public static CreateCouponCommand ToCommand(this CreateCouponRequest request) =>
    new(
        request.Code,
        request.Type,
        request.AmountOrPercentage,
        request.MinimumSpend,
        request.Currency,
        request.ExpirationDateUtc,
        request.MaxDiscountAmount);
}