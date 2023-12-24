using AgencyService.DTO;
using AgencyService.Models;

namespace AgencyService.Interfaces
{
    public interface IPaymentServiceService
    {
        Task<List<PaymentServiceDto>> GetAll(int agencyId);
        Task<List<PaymentServiceDto>> SubscribePaymentService(List<PaymentServiceDto> paymentServicesDto, int agencyId);
        Task<PaymentService> CreatePaymentServiceDto(CreatePaymentServiceDto paymentServiceDto, int agencyId);
    }
}
