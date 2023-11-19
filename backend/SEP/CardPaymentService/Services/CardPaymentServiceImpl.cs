using CardPaymentService.Interfaces;

namespace CardPaymentService.Services
{
    public class CardPaymentServiceImpl : ICardPaymentService
    {
        public Task<string> MakePayment()
        {
            string paymentMessage = "Credit Card Payment was successful!";
            return Task.FromResult(paymentMessage);
        }
    }
}
