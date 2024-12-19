using System.Linq.Expressions;

namespace Application.Contracts.Persistance
{
    public interface IGenericRepository<T, TId> where T : class where TId : struct
    {
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> AnyAsync(TId id);
    }
}
