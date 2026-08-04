using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Cart.Modules.Application.Features.Responses;

namespace Ecommerce.Cart.Modules.Application.Features.GetCarts;

public sealed record GetCartsQuery(int Page, int PageSize) : PagedQuery<CartListResponse>;
