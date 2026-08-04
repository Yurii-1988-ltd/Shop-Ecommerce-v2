using MongoDB.Driver;

namespace Ecommerce.Cart.Modules.Application.Abstractions.Data;

public interface ICartDatabase
{
    IMongoCollection<Domain.Entities.Cart>Carts { get; set; }
    
}