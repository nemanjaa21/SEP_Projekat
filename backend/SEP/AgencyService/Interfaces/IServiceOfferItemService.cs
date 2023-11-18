using AgencyService.Models;

namespace AgencyService.Interfaces
{
    public interface IServiceOfferItemService
    {
        Task<List<ServiceOfferItem>> GetAllServiceOfferItem();
    }
}
