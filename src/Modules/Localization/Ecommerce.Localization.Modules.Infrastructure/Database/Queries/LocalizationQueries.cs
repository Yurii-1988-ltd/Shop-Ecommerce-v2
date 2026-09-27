using Dapper;
using Ecommerce.Application.Pagination;
using Ecommerce.Localization.Modules.Application.DTO;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Infrastructure.Database.Queries;

internal sealed class LocalizationQueries(
    [FromKeyedServices("localization")] NpgsqlDataSource dataSource)
    : ILocalizationQueries
{
    public async Task<string?> GetAsync(
        string key,
        CultureInfo culture,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT "Value"
            FROM "translations"
            WHERE "Key" = @Key
              AND "CultureCode" = @CultureCode
            LIMIT 1;
            """;

        await using var connection =
            await dataSource.OpenConnectionAsync(
                cancellationToken);

        var command = new CommandDefinition(
            sql,
            new
            {
                Key = key,
                CultureCode = culture.Name
            },
            cancellationToken: cancellationToken);

        return await connection.QuerySingleOrDefaultAsync<string>(
            command);
    }

    public async Task<TranslationResponse?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        const string sql = """
        SELECT
            "Id",
            "Key",
            "CultureCode",
            "Value",
            "Module",
            "Description"
        FROM "translations"
        WHERE "Id" = @Id;
        """;

        await using var connection =
            await dataSource.OpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            sql,
            new { Id = id },
            cancellationToken: cancellationToken);

        return await connection.QuerySingleOrDefaultAsync<TranslationResponse>(
            command);
    }

 
        public async Task<PagedResult<TranslationResponse>> GetPagedAsync(
    string? key,
    string? cultureCode,
    string? module,
    int page,
    int pageSize,
    CancellationToken cancellationToken = default)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var offset = (page - 1) * pageSize;

        const string countSql = """
        SELECT COUNT(*)
        FROM "translations"
        WHERE (@Key IS NULL OR "Key" ILIKE '%' || @Key || '%')
          AND (@CultureCode IS NULL OR "CultureCode" = @CultureCode)
          AND (@Module IS NULL OR "Module" = @Module);
        """;

        const string dataSql = """
        SELECT
            "Id",
            "Key",
            "CultureCode",
            "Value",
            "Module",
            "Description"
        FROM "translations"
        WHERE (@Key IS NULL OR "Key" ILIKE '%' || @Key || '%')
          AND (@CultureCode IS NULL OR "CultureCode" = @CultureCode)
          AND (@Module IS NULL OR "Module" = @Module)
        ORDER BY "Key", "CultureCode"
        OFFSET @Offset
        LIMIT @PageSize;
        """;

        await using var connection =
            await dataSource.OpenConnectionAsync(
                cancellationToken);

        var parameters = new
        {
            Key = string.IsNullOrWhiteSpace(key)
                ? null
                : key.Trim(),

            CultureCode = string.IsNullOrWhiteSpace(cultureCode)
                ? null
                : cultureCode.Trim(),

            Module = string.IsNullOrWhiteSpace(module)
                ? null
                : module.Trim(),

            Offset = offset,
            PageSize = pageSize
        };

        var totalCount = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                countSql,
                parameters,
                cancellationToken: cancellationToken));

        var items = (
            await connection.QueryAsync<TranslationResponse>(
                new CommandDefinition(
                    dataSql,
                    parameters,
                    cancellationToken: cancellationToken))
            ).AsList();

        return new PagedResult<TranslationResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}

