

namespace Ecommerce.Cart.Modules.Presentation.Carts;

public class CreateCouponEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Ручное создание купона
        app.MapPost("/api/admin/coupons", async (
            CreateCouponRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateCouponCommand(
                request.Code,
                request.Type,
                request.AmountOrPercentage,
                request.MinimumSpend,
                request.Currency,
                request.ExpirationDateUtc,
                request.MaxDiscountAmount);

            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new { error = result.Error.Code, description = result.Error.Description });
            }

            return Results.Created($"/api/admin/coupons/{result.Value}", new CouponResponse(result.Value, request.Code));
        })
        .WithName("CreateCoupon")
        .WithTags("Coupons");


    }

}