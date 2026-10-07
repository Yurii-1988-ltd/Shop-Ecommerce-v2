namespace Ecommerce.Shared.Contracts.Orders;

public interface ICurrentUser
{
    Guid UserId { get; }
   // Guid GuestId { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(string role);
}
