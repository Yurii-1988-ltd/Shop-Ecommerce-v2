

namespace Ecommerce.Cart.Modules.Application.Features.Cart.Mapping;

/// <summary>
/// Provides extension methods for mapping Cart domain entities to Application layer responses.
/// </summary>
public static class CartMapping
{
    /// <summary>
    /// Maps a <see cref="Domain.Entities.Cart"/> aggregate to a <see cref="CartResponse"/> DTO.
    /// </summary>
    /// <param name="cart">The cart aggregate to map.</param>
    /// <returns>A fully populated <see cref="CartResponse"/> object.</returns>
    public static CartResponse ToResponse(this Domain.Entities.Cart cart)
    {
        var subtotalResult = cart.GetSubtotal();
        var discountResult = cart.GetDiscountTotal();
        var totalResult = cart.GetTotalCost();

        // 1. Determine currency: prioritize calculated financial result, fallback to first item currency or default (UAH)
        string currency = subtotalResult.IsSuccess
            ? subtotalResult.Value.Currency
            : cart.Items.FirstOrDefault()?.Currency ?? Ecommerce.Domain.Constants.CurrencyConstant.UAH;

        // 2. Safe extraction of financial amounts
        decimal subtotalAmount = subtotalResult.IsSuccess ? subtotalResult.Value.Amount : 0m;
        decimal discountAmount = discountResult.IsSuccess ? discountResult.Value.Amount : 0m;
        decimal totalAmount = totalResult.IsSuccess ? totalResult.Value.Amount : 0m;

        // 3. Map individual cart line items
        var itemResponses = cart.Items
            .Select(item => new CartItemResponse(
                ProductId: item.ProductId,
                Name: item.Name,
                Sku: item.Sku,
                Price: item.Price.Amount,
                Currency: item.Currency,
                Quantity: item.Quantity,
                TotalPrice: item.TotalPrice.Amount))
            .ToList();

        // 4. Construct and return the final DTO
        return new CartResponse(
            Id: cart.Id,
            CustomerId: cart.CustomerId,
            GuestId: cart.GuestId,
            Items: itemResponses,
            TotalItems: cart.Items.Sum(i => i.Quantity),
            Subtotal: subtotalAmount,
            Discount: discountAmount,
            TotalAmount: totalAmount,
            Currency: currency,
            Code: cart.AppliedCouponCode ?? string.Empty
        );
    }

    /// <summary>
    /// Maps a collection of <see cref="Domain.Entities.Cart"/> aggregates to a read-only list of <see cref="CartResponse"/> DTOs.
    /// </summary>
    /// <param name="carts">The collection of carts to map.</param>
    /// <returns>A read-only list of mapped <see cref="CartResponse"/> DTOs.</returns>
    public static IReadOnlyList<CartResponse> ToListResponse(this IEnumerable<Domain.Entities.Cart> carts)
    {
        return carts.Select(ToResponse).ToList();
    }
}