using BankService.DTO;
using BankService.Enums;
using shared;

namespace BankService.Interfaces
{
    public interface IPSPService
    {
        Task<PaymentResponse> ProcessPayment(PaymentRequest paymentRequest);
        PSPResponseDTO GenerateResponseBasedOnURL(Url url);
    }
}
