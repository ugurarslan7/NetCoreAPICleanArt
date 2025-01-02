using Domain.Entities;

namespace Application.Contracts.Persistance
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {
        Task<Category?> GetCategoryWithProductAsync(int id);
        Task<List<Category>> GetCategoryWithProductAsync();
    }
}
