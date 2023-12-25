using BankService.Interfaces;
using BankService.Models;
using Microsoft.EntityFrameworkCore;

namespace BankService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContext _dbContext;
        public IGenericRepository<User> UsersRepository { get; }
        public IGenericRepository<Account> AccountsRepository { get; }
        public IGenericRepository<Transaction> TransactionsRepository { get; }
        public IGenericRepository<Merchant> MerchantsRepository { get; }
        public IGenericRepository<Card> CardsRepository { get; }

        public UnitOfWork(
            DbContext dbContext,
            IGenericRepository<User> userRepository,
            IGenericRepository<Account> accountRepository,
            IGenericRepository<Transaction> transactionRepository,
            IGenericRepository<Merchant> merchantRepository,
            IGenericRepository<Card> cardRepository)
        {
            _dbContext = dbContext;
            UsersRepository = userRepository;
            AccountsRepository = accountRepository;
            TransactionsRepository = transactionRepository;
            MerchantsRepository = merchantRepository;
            CardsRepository = cardRepository;
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
