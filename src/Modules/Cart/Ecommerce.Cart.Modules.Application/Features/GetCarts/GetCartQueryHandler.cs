
using Ecommerce.Application.Pagination;
using Ecommerce.Cart.Modules.Application.Features.Cart.Mapping;


namespace Ecommerce.Cart.Modules.Application.Features.GetCarts;

internal sealed class GetCartsQueryHandler(ICartRepository cartRepository)
    : IQueryHandler<GetCartsQuery, PagedResult<CartResponse>>
{
    public async Task<Result<PagedResult<CartResponse>>> Handle(
        GetCartsQuery request,
        CancellationToken cancellationToken)
    {
        var (carts, totalCount) = await cartRepository.GetPagedAsync(
            request.Page,
            request.PageSize,
            cancellationToken);

        var items = carts.Select(x => x.ToResponse()).ToList();

        return Result.Success(new PagedResult<CartResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}