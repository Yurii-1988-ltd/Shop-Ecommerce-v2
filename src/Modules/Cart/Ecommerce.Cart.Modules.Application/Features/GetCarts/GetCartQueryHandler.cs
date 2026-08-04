using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Cart.Modules.Application.Features.GetCarts;
using Ecommerce.Cart.Modules.Application.Features.Responses;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

internal sealed class GetCartsQueryHandler(ICartRepository cartRepository)
    : IQueryHandler<GetCartsQuery, PagedResult<CartListResponse>>
{
    public async Task<Result<PagedResult<CartListResponse>>> Handle(
        GetCartsQuery request,
        CancellationToken cancellationToken)
    {
        var (carts, totalCount) = await cartRepository.GetPagedAsync(
            request.Page,
            request.PageSize,
            cancellationToken);

        var items = carts.Select(x => x.ToListResponse()).ToList();

        return Result.Success(new PagedResult<CartListResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}