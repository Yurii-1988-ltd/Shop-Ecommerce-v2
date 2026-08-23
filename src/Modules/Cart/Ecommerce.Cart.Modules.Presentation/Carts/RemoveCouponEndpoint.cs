// DTO без поля Code
using Ecommerce.Cart.Modules.Application.RemoveCoupon;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

public class RemoveCouponEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/carts/{customerId:guid}/coupon", async (
           Guid customerId,
           ISender sender,
           CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new RemoveCouponCommand(customerId),
                cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error.Code, description = result.Error.Description });
        })
       .WithName("RemoveCoupon")
       .WithTags(Tags.Carts);
    }
}
