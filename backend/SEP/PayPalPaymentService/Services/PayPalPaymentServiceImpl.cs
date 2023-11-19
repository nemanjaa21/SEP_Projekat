using PayPalPaymentService.Interfaces;

namespace PayPalPaymentService.Services
{
    public class PayPalPaymentServiceImpl : IPayPalPaymentService
    {
        public Task<string> MakePayment()
        {
            string paymentMessage = "PayPal Payment was successful!";
            return Task.FromResult(paymentMessage);
        }
    }
}
