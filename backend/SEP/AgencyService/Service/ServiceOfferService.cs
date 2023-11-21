using AgencyService.Interfaces;
using AgencyService.Models;

namespace AgencyService.Service
{
    public class ServiceOfferService : IServiceOfferService
    {
        private IUnitOfWork _unitOfWork;

        public ServiceOfferService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceOffer> GetServiceOfferById(int id)
        {
            ServiceOffer? ret = await _unitOfWork.ServiceOfferRepository.Get(x => x.Id == id, new List<string>() { "ServiceOfferItems" });
            if (ret == null)
            {
                return null!;
            }

            return ret;
        }

        public async Task<ServiceOffer> CreateServiceOffer(Dictionary<int, bool> ids)
        {
            List<ServiceOfferItem> offerItems = new List<ServiceOfferItem>();
            double totalPrice = 0;

            foreach (var kvp in ids)
            {
                var id = kvp.Key;
                var isMonthly = kvp.Value;

                var item = await _unitOfWork.ServiceOfferItemRepository.Get(x => x.Id == id);
                if (item != null)
                {
                    offerItems.Add(item);

                    totalPrice += isMonthly ? item.MonthlyPrice : item.YearlyPrice;
                }
            }

            ServiceOffer serviceOffer = new ServiceOffer()
            {
                ServiceOfferItems = offerItems,
                TotalPrice = totalPrice
            };

            await _unitOfWork.ServiceOfferRepository.Insert(serviceOffer);
            await _unitOfWork.Save();

            return serviceOffer;
        }
    }
}
