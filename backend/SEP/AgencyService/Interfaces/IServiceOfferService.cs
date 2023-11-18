using AgencyService.Models;

namespace AgencyService.Interfaces
{
    public interface IServiceOfferService
    {
        Task<ServiceOffer> GetServiceOfferById(int id);
        Task<ServiceOffer> CreateServiceOffer(Dictionary<int, bool> ids);
    }
}
