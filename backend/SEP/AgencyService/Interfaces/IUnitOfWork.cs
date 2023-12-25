using AgencyService.Models;

namespace AgencyService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ServiceOffer> ServiceOfferRepository { get; }
        IGenericRepository<ServiceOfferItem> ServiceOfferItemRepository { get; }
        IGenericRepository<Agency> AgencyRepository { get; }
        IGenericRepository<PaymentService> PaymentServiceRepository { get; }
        Task Save();
    }
}
