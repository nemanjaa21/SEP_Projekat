using Microsoft.EntityFrameworkCore;
using PaymentCardCenterService.Interfaces;
using PaymentCardCenterService.Models;

namespace PaymentCardCenterService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContext _dbContext;
        public IGenericRepository<Bank> BankRepository { get; }

        public UnitOfWork(DbContext dbContext, IGenericRepository<Bank> bankRepository)
        {
            _dbContext = dbContext;
            BankRepository = bankRepository;
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
