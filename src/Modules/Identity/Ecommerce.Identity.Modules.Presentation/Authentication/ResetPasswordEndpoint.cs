

internal sealed class ResetPasswordEndpoint
{
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/reset-password", async (ResetPasswordCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Authentication);
    }
}