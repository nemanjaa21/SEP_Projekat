using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Models;

namespace AgencyService.Service
{
    public class AgencyService : IAgencyService
    {
        private IUnitOfWork _unitOfWork;

        public AgencyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Agency> GetAgencyById(int id)
        {
            Agency? agency = await _unitOfWork.AgencyRepository.Get(x => x.Id == id);
            if (agency == null)
                throw new Exception("Agency doesn't exist");

            return agency;
        }

        public async Task<Agency> RegisterAgency(RegisterAgencyDto registerAgencyDto, int userId)
        {
            Agency newAgency = new Agency() { Name = registerAgencyDto.Name! };
            await _unitOfWork.AgencyRepository.Insert(newAgency);
            await _unitOfWork.Save();
            return newAgency;
        }
    }
}
