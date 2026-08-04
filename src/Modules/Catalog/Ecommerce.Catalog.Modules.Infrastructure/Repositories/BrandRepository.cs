using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Mongo;


namespace Ecommerce.Catalog.Modules.Infrastructure.Repositories
{
    internal sealed class BrandRepository : IBrandRepository
    {
        private readonly IMongoCollection<Brand> _collection;

        public BrandRepository(IMongoContext context)
        {
            _collection = context.Database.GetCollection<Brand>("Brands");
        }
        public  Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return  _collection.DeleteOneAsync(
            x => x.Id == id, cancellationToken);
        }

        public async Task<Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(Brand brand, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(brand, cancellationToken: cancellationToken);
        }

        public Task ReplaceAsync(Brand brand, CancellationToken cancellationToken)
        {
            return _collection.ReplaceOneAsync(
            x => x.Id == brand.Id,
            brand);
        }
    }
}
