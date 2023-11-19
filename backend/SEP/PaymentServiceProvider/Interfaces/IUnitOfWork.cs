using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<PaymentService> PaymentServiceRepository { get; }
        Task Save();
    }
}
