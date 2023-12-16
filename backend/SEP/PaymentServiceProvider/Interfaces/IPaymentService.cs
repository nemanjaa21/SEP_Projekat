using PaymentServiceProvider.Models;
using shared;

namespace PaymentServiceProvider.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentService>> GetAllPaymentMethods();
        Task<PaymentService> GetPaymentService(int id);
        Task<PaymentResponse> ProcessPayment(PSPRequest pspRequest, string apiKey);
    }
}
