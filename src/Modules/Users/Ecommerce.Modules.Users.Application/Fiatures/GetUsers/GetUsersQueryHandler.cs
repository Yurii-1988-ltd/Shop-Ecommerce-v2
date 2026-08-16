using Dapper;
using Ecommerce.Application.Pagination;

namespace Ecommerce.Modules.Users.Application.Fiatures.GetUsers;

public sealed class GetUsersQueryHandler(
    IDbConnectionFactory dbConnection)
    : IQueryHandler<GetUsersQuery, PagedResult<UserResponse>>
{
    public async Task<Result<PagedResult<UserResponse>>> Handle(
        GetUsersQuery query,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection =
            await dbConnection.OpenConnectionAsync();

        const string sql =
            $"""
            SELECT
                Id AS {nameof(UserResponse.Id)},
                Email AS {nameof(UserResponse.Email)},
                FirstName AS {nameof(UserResponse.FirstName)},
                LastName AS {nameof(UserResponse.LastName)},
                CreatedAtUtc AS {nameof(UserResponse.CreatedAtUtc)}
            FROM users
            ORDER BY CreatedAtUtc DESC
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY;

            SELECT COUNT(*)
            FROM users;
            """;

        var offset = (query.Page - 1) * query.PageSize;

        using var multi = await connection.QueryMultipleAsync(
            sql,
            new
            {
                Offset = offset,
                query.PageSize
            });

        var users = (await multi.ReadAsync<UserResponse>())
            .AsList();

        var totalCount = await multi.ReadSingleAsync<int>();

        var result = new PagedResult<UserResponse>
        {
            Items = users,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount
        };

        return result;
    }
}