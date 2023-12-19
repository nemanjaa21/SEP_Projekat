using AgencyService.DTO;
using AgencyService.Interfaces;
using AgencyService.Models;
using AutoMapper;

namespace AgencyService.Service
{
    public class PaymentServiceService : IPaymentServiceService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PaymentServiceDto>> GetAll(int agencyId)
        {
            var allPaymentService = await _unitOfWork.PaymentServiceRepository.GetAll();
            var subscribedPaymentService = new List<PaymentServiceDto>();

            foreach (var paymentService in allPaymentService)
            {
                bool temp = false;
                if (paymentService.Agencies.Find(x => x.Id == agencyId) != null)
                {
                    temp = true;
                }

                var pS = new PaymentServiceDto() {Id = paymentService.Id, Name = paymentService.Name, Subscribed = temp};
                subscribedPaymentService.Add(pS);
            }
            
            return subscribedPaymentService;
        }

        public async Task<List<PaymentServiceDto>> SubscribePaymentService(List<PaymentServiceDto> paymentServicesDto, int agencyId)
        {
            var agency = await _unitOfWork.AgencyRepository.Get(x=> x.Id == agencyId);
            var allPaymentService = await _unitOfWork.PaymentServiceRepository.GetAll();

            foreach (var paymentService in paymentServicesDto)
            {
                if (paymentService.Subscribed)
                {
                    var item = allPaymentService.ToList().Find(x => x.Id == paymentService.Id);
                    item.Agencies.Add(agency);
                    _unitOfWork.PaymentServiceRepository.Update(item);
                }
                else
                {
                    var item = allPaymentService.ToList().Find(x => x.Id == paymentService.Id);
                    item.Agencies.Remove(agency);
                    _unitOfWork.PaymentServiceRepository.Update(item);
                }
            }

            _unitOfWork.Save();
            return paymentServicesDto;
        }
    }
}
