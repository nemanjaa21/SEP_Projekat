using Microsoft.EntityFrameworkCore;
using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContext _dbContext;
        public IGenericRepository<PaymentService> PaymentServiceRepository { get; }

        public UnitOfWork(DbContext dbContext,IGenericRepository<PaymentService> paymentServiceRepository)
        {
            _dbContext = dbContext;
            PaymentServiceRepository = paymentServiceRepository;
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
