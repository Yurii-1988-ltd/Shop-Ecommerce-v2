using Ecommerce.Domain.Domain;
using Ecommerce.Promotions.Modules.Application.Features.DeleteCoupon;
using MediatR;

namespace Ecommerce.Promotions.Modules.Presentation;

internal sealed class DeleteCouponEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/admin/coupons/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new DeleteCouponCommand(id),
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

            return Results.NoContent();
        })
        .WithName("DeleteCoupon")
        .WithTags("Coupons")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}