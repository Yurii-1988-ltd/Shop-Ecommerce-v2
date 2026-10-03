
using Ecommerce.Cart.Modules.Application.Features.ApplyCoupon;
using Ecommerce.Cart.Modules.Presentation;


namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class ApplyCouponEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts/{customerId:guid}/coupon", async (
            Guid customerId,
            ApplyCouponRequest request,
            ISender sender,
            CancellationToken cancellationToken = default) =>
        {
            var command = new ApplyCouponCommand(customerId, request.Code);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new { 
                    error = result.Error.Code,
                    description = result.Error.Description
                });
            }

            return Results.NoContent();
        })
        .WithName("ApplyCoupon")
        .WithTags(Tags.Carts);
    }
    public record ApplyCouponRequest(string Code);
}
