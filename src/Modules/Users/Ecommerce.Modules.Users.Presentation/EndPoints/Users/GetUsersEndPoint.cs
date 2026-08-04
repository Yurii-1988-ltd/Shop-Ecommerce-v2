using Ecommerce.Modules.Users.Application.Fiatures.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users
{
    internal sealed class GetUsersEndPoint
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/users", async (ISender sender) =>
            {
                var result = await sender.Send(new GetUsersQuery());
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            })
            .WithTags(Tags.Users);
        }
    }
}
