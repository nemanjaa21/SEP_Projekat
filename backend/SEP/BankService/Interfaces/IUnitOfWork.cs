using BankService.Models;

namespace BankService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UsersRepository { get; }
        IGenericRepository<Account> AccountsRepository { get; }
        IGenericRepository<Transaction> TransactionsRepository { get; }
        IGenericRepository<Merchant> MerchantsRepository { get; }
        IGenericRepository<Card> CardsRepository { get; }
        Task Save();
    }
}
