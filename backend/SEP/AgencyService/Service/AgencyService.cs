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
            return await _unitOfWork.AgencyRepository.Get(x=> x.Id == id);
        }

        public async Task<Agency> RegisterAgency(RegisterAgencyDto registerAgencyDto, int userId)
        {
            Agency newAgency = new Agency() { Name = registerAgencyDto.Name };
            _unitOfWork.AgencyRepository.Insert(newAgency);
            _unitOfWork.Save();
            return newAgency;
        }
    }
}
