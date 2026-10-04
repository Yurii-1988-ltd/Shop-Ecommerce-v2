using Ecommerce.Promotions.Modules.Presentation;

internal sealed class CreateCouponEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
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

            return Results.Created(
                $"/api/admin/coupons/{result.Value}",
                new CouponResponse(
                    result.Value,
                    request.Code));
        })
        .WithTags(Tags.Promotions)
        .Accepts<CreateCouponRequest>("application/json")
        .Produces<CouponResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }

    public sealed record CreateCouponRequest(
        string Code,
        CouponType Type,
        decimal AmountOrPercentage,
        decimal MinimumSpend,
        string Currency,
        DateTime ExpirationDateUtc,
        decimal? MaxDiscountAmount = null);
}