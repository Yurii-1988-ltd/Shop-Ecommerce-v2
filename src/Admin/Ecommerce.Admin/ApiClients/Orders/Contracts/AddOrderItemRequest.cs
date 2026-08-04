namespace Ecommerce.Admin.ApiClients.Orders.Contracts
{
    public sealed record AddOrderItemRequest(Guid ProductId,
                                    int Quantity);
    
}
