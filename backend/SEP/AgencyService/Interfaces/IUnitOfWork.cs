using AgencyService.Models;

namespace AgencyService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ServiceOffer> ServiceOfferRepository { get; }
        IGenericRepository<ServiceOfferItem> ServiceOfferItemRepository { get; }
        Task Save();
    }
}
