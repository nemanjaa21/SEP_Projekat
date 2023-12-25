using BitcoinPaymentService.Interfaces;
using BitcoinPaymentService.Models;
using Microsoft.EntityFrameworkCore;

namespace BankService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContext _dbContext;
        public IGenericRepository<User> UsersRepository { get; }
        public IGenericRepository<Transaction> TransactionsRepository { get; }
        public IGenericRepository<Merchant> MerchantsRepository { get; }

        public UnitOfWork(
            DbContext dbContext,
            IGenericRepository<User> userRepository,
            IGenericRepository<Transaction> transactionRepository,
            IGenericRepository<Merchant> merchantRepository)
        {
            _dbContext = dbContext;
            UsersRepository = userRepository;
            TransactionsRepository = transactionRepository;
            MerchantsRepository = merchantRepository;
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

        public async Task DeleteTransaction(Transaction transaction)
        {
            TransactionsRepository.Delete(transaction);

            await _dbContext.SaveChangesAsync();
        }
    }
}
