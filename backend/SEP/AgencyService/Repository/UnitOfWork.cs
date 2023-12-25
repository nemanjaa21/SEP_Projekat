using AgencyService.Interfaces;
using AgencyService.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContext _dbContext;
        public IGenericRepository<ServiceOffer> ServiceOfferRepository { get; }
        public IGenericRepository<ServiceOfferItem> ServiceOfferItemRepository { get; }
        public IGenericRepository<Agency> AgencyRepository { get; }
        public IGenericRepository<PaymentService> PaymentServiceRepository { get; }

        public UnitOfWork(DbContext dbContext, IGenericRepository<ServiceOffer> serviceOfferRepository, IGenericRepository<ServiceOfferItem> serviceOfferItemRepository, IGenericRepository<Agency> agencyRepository, IGenericRepository<PaymentService> paymentServiceRepository)
        {
            _dbContext = dbContext;
            ServiceOfferRepository = serviceOfferRepository;
            ServiceOfferItemRepository = serviceOfferItemRepository;
            AgencyRepository = agencyRepository;
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
