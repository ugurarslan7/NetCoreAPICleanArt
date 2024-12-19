using Domain.Entities;

namespace Application.Contracts.Persistance
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count);

    }
}
