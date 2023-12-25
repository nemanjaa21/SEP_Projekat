using PaymentCardCenterService.Models;

namespace PaymentCardCenterService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Bank> BankRepository { get; }
        Task Save();
    }
}
