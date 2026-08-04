

namespace Ecommerce.Application.Pagination;

public sealed record PageRequest
{
    private const int MaxPageSize = 100;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public int Skip => (Page - 1) * PageSize;
    public int Take => Math.Min(PageSize, MaxPageSize);
}
