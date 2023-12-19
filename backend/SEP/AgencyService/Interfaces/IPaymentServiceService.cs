using AgencyService.DTO;

namespace AgencyService.Interfaces
{
    public interface IPaymentServiceService
    {
        Task<List<PaymentServiceDto>> GetAll(int agencyId);
        Task<List<PaymentServiceDto>> SubscribePaymentService(List<PaymentServiceDto> paymentServicesDto, int agencyId);
    }
}
