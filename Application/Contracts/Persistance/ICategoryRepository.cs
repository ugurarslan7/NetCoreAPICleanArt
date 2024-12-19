using Domain.Entities;

namespace Application.Contracts.Persistance
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {
        Task<Category?> GetCategoriyWithProductAsync(int id);
        IQueryable<Category> GetCategoriyWithProduct();
    }
}
