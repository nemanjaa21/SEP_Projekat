using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Models;
using AutoMapper;

namespace AgencyService.Service
{
    public class ServiceOfferItemService : IServiceOfferItemService
    {
        private IUnitOfWork _unitOfWork;

        public ServiceOfferItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ServiceOfferItem>> GetAllServiceOfferItem()
        {
            var items = await _unitOfWork.ServiceOfferItemRepository.GetAll();
            return items.ToList();
        }

        public async Task<ServiceOfferItem> GetServiceOfferItemById(int id)
        {
            ServiceOfferItem? ret = await _unitOfWork.ServiceOfferItemRepository.Get(x => x.Id == id);
            if(ret == null)
            {
                return null!;
            }

            return ret;
        }

        public async Task<ServiceOfferItem> CreateServiceOfferItem(CreateServiceOfferItemDto serviceOfferItemDto, int agencyId)
        {
            var Agency = await _unitOfWork.AgencyRepository.Get(x=> x.Id == agencyId);
            var serviceOfferItem = new ServiceOfferItem() { OfferName = serviceOfferItemDto.OfferName, MonthlyPrice = serviceOfferItemDto.MonthlyPrice, YearlyPrice = serviceOfferItemDto.YearlyPrice, IsAccepted = false, AgencyId = agencyId };
            await _unitOfWork.ServiceOfferItemRepository.Insert(serviceOfferItem);
            await _unitOfWork.Save();
            return serviceOfferItem;
        }
    }
}
