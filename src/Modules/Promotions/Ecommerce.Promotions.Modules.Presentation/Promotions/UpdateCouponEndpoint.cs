using Ecommerce.Promotions.Modules.Application.Features.UpdateCoupon;

namespace Ecommerce.Promotions.Modules.Presentation;

internal sealed class UpdateCouponEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/admin/coupons/{id:guid}", async (
            Guid id,
            UpdateCouponRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateCouponCommand(
                id,
                request.Code,
                request.Type,
                request.AmountOrPercentage,
                request.MinimumSpend,
                request.Currency,
                request.ExpirationDateUtc,
                request.MaxDiscountAmount);

            var result = await sender.Send(
                command,
                cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    error = result.Error.Code,
                    description = result.Error.Description
                });
            }

            return Results.NoContent();
        })
        .WithName("UpdateCoupon")
        .WithTags("Coupons")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }

    public sealed record UpdateCouponRequest(
        string Code,
        CouponType Type,
        decimal AmountOrPercentage,
        decimal MinimumSpend,
        string Currency,
        DateTime ExpirationDateUtc,
        decimal? MaxDiscountAmount = null);
}