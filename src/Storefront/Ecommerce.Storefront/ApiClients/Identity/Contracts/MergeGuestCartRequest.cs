namespace Ecommerce.Storefront.ApiClients.Identity.Contracts
{
    public sealed record MergeGuestCartRequest(Guid GuestId, Guid CustomerId);
   
}
