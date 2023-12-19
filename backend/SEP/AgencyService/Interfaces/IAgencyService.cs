using AgencyService.DTO;
using AgencyService.Models;

namespace AgencyService.Interfaces
{
    public interface IAgencyService
    {
        Task<Agency> GetAgencyById(int id);
        Task<Agency> RegisterAgency(RegisterAgencyDto registerAgencyDto, int userId);
    }
}
