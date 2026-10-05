using Ecommerce.Modules.Users.Presentation;

internal sealed class GetUserEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetUserQuery(id));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Users);
    }
}