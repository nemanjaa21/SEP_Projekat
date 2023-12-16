using shared;

namespace BankService.Interfaces
{
    public interface IPSPService
    {
        Task<PaymentResponse> ProcessPayment(PaymentRequest paymentRequest);
    }
}
