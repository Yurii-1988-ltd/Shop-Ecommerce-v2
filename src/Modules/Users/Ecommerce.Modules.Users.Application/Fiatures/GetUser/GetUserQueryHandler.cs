using Ecommerce.Modules.Users.Application.Fiatures.GetUser;
using Ecommerce.Modules.Users.Domain.Errors;

public sealed class GetUserQueryHandler(IDbConnectionFactory dbConnection)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
         using IDbConnection connection =
            await dbConnection.OpenConnectionAsync();

        const string sql = $"""
            SELECT 
               e.Id AS {nameof(UserResponse.Id)},
               e.Email AS {nameof(UserResponse.Email)},
               e.FirstName AS {nameof(UserResponse.FirstName)},
               e.LastName AS {nameof(UserResponse.LastName)},
               e.CreatedAtUtc AS {nameof(UserResponse.CreatedAtUtc)}
            FROM users e
            WHERE e.Id = @userId
        """;

        var userResponse = await connection.QuerySingleOrDefaultAsync<UserResponse>(
            sql,
            new { request.userId });

        if (userResponse is null)
        {
            return Result<UserResponse>.Failure(
    UserErrors.NotFound(request.userId));
        }

        return Result<UserResponse>.Success(userResponse);
    }
}