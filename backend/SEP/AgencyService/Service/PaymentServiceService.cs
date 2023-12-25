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
            var agency = await _unitOfWork.AgencyRepository.Get(x => x.Id == agencyId, new List<string>() { "PaymentServices" });
            var subscribedPaymentService = new List<PaymentServiceDto>();

            foreach (var paymentService in allPaymentService)
            {
                var pS = new PaymentServiceDto() {Id = paymentService.Id, Name = paymentService.Name!, Subscribed = paymentService.Subscribed};
                subscribedPaymentService.Add(pS);
            }         
            return subscribedPaymentService;
        }

        public async Task<List<PaymentServiceDto>> SubscribePaymentService(List<PaymentServiceDto> paymentServicesDto, int agencyId)
        {
            var allPaymentServices = await _unitOfWork.PaymentServiceRepository.GetAll();

            foreach (var paymentService in paymentServicesDto)
            {
                var item = allPaymentServices.FirstOrDefault(x => x.Id == paymentService.Id);
                if (item == null)
                {
                    continue;
                }

               item.Subscribed = paymentService.Subscribed;
                _unitOfWork.PaymentServiceRepository.Update(item);
            }
            await _unitOfWork.Save();
            return paymentServicesDto;
        }



        public async Task<PaymentService> CreatePaymentServiceDto(CreatePaymentServiceDto paymentServiceDto, int agencyId)
        {
            var Agency = await _unitOfWork.AgencyRepository.Get(x => x.Id == agencyId);
            var paymentService = new PaymentService() { Name = paymentServiceDto.Name, AgencyId = Agency!.Id, Subscribed = true };
            await _unitOfWork.PaymentServiceRepository.Insert(paymentService);
            await _unitOfWork.Save();
            return paymentService;
        }
    }
}
