using AgencyService.DTO;
using AgencyService.Models;

namespace AgencyService.Interfaces
{
    public interface IServiceOfferItemService
    {
        Task<ServiceOfferItem> GetServiceOfferItemById(int id);
        Task<List<ServiceOfferItem>> GetAllServiceOfferItem();
        Task<ServiceOfferItem> CreateServiceOfferItem(CreateServiceOfferItemDto serviceOfferItemDto, int agencyId);
    }
}
