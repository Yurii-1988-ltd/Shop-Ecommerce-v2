
    namespace Ecommerce.Admin.ApiClients.Carts.Contracts;

    public sealed record AddCartItemRequest(
    
         Guid ProductId ,
         string Name ,
        decimal Price,
       string Currency ,
        int  Quantity 
    );

        

