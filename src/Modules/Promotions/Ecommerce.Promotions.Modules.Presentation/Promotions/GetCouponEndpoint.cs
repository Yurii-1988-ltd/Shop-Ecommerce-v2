
using Ecommerce.Domain.Domain;
using Ecommerce.Promotions.Modules.Application.Features.GetCoupon;
using CouponResponse = Ecommerce.Promotions.Modules.Application.Features.GetCoupon.CouponResponse;

namespace Ecommerce.Promotions.Modules.Presentation.Promotions;

internal sealed class GetCouponEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/coupons/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetCouponQuery(id),
                cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.Type == ErrorType.NotFound
                    ? Results.NotFound(new
                    {
                        error = result.Error.Code,
                        description = result.Error.Description
                    })
                    : Results.BadRequest(new
                    {
                        error = result.Error.Code,
                        description = result.Error.Description
                    });
            }

            return Results.Ok(result.Value);
        })
      
        .WithTags(Tags.Promotions)
        .Produces<CouponResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
