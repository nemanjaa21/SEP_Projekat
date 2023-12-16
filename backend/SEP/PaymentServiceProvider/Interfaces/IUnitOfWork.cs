using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<PaymentService> PaymentServiceRepository { get; }
        IGenericRepository<Merchant> MerchantRepository { get; }
        Task Save();
    }
}
