using AuthService.Interfaces;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContext _dbContext;
        public IGenericRepository<User> UserRepository { get; }

        public UnitOfWork(DbContext dbContext,IGenericRepository<User> userRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
