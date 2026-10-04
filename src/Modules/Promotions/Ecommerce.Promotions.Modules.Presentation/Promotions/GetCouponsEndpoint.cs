using Ecommerce.Application.Pagination;
using Ecommerce.Promotions.Modules.Application.Features.GetCoupons;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Promotions.Modules.Presentation.Promotions;

internal sealed class GetCouponsEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/admin/coupons", async (
            [FromQuery] int page,
            [FromQuery] int pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetCouponsQuery(page, pageSize),
                cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    error = result.Error.Code,
                    description = result.Error.Description
                });
            }

            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Promotions)
        .WithName("GetCoupons")
        .Produces<PagedResult<CouponResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}