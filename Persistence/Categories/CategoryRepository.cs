using Application.Contracts.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Categories
{
    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
    {


        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Category?> GetCategoryWithProductAsync(int id)
        {
            return Context.Categories.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Category> GetCategoryWithProduct()
        {
            return Context.Categories.Include(p => p.Products).AsQueryable();
        }

        public Task<List<Category>> GetCategoryWithProductAsync()
        {
           return Context.Categories.Include(p=>p.Products).ToListAsync();
        }
    }
}
