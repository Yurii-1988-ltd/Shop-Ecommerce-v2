

namespace Ecommerce.Cart.Modules.Application.Abstractions;

public sealed record CartOwner(Guid? CustomerId,Guid? GuestId)
{
    public bool IsGuest => GuestId.HasValue;
    public bool IsCustomer => CustomerId.HasValue;
}
