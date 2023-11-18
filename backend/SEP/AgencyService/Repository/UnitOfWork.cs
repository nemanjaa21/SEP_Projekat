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

        public UnitOfWork(DbContext dbContext, IGenericRepository<ServiceOffer> serviceOfferRepository, IGenericRepository<ServiceOfferItem> serviceOfferItemRepository)
        {
            _dbContext = dbContext;
            ServiceOfferRepository = serviceOfferRepository;
            ServiceOfferItemRepository = serviceOfferItemRepository;
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
