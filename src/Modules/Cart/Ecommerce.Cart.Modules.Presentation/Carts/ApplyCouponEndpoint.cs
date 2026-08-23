using Ecommerce.Cart.Modules.Presentation;
using Ecommerce.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Cart.Modules.Application.Features.ApplyCoupon;

public class ApplyCouponEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts/{customerId:guid}/coupon", async (
            Guid customerId,
            ApplyCouponRequest request,
            ISender sender) =>
        {
            var command = new ApplyCouponCommand(customerId, request.Code);
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(new { error = result.Error });
            }

            return Results.Ok();
        })
        .WithName("ApplyCoupon")
        .WithTags(Tags.Carts);
    }
}
public record ApplyCouponRequest(string Code);
