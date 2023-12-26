using PayPal.Api;
using PayPalPaymentService.Dto;

namespace PayPalPaymentService.Interfaces
{
    public interface IPayPalPaymentService
    {
        Task<string> MakePayment(ServiceOfferDto offerDto);
        Task<Payment> SuccessPayPalPayment(string paymentId, string payerId, int orderId);
        Task CancelPayPalPayment(int orderId);
    }
}
