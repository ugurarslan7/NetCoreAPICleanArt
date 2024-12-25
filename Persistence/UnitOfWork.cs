using Application.Contracts.Persistance;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<int> SaveChangesAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }
    }
}
