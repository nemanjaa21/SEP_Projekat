using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentService>> GetAllPaymentMethods();
    }
}
