using BitcoinPaymentService.Models;

namespace BitcoinPaymentService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UsersRepository { get; }
        IGenericRepository<Transaction> TransactionsRepository { get; }
        IGenericRepository<Merchant> MerchantsRepository { get; }
        Task DeleteTransaction(Transaction transaction);
        Task Save();
    }
}
