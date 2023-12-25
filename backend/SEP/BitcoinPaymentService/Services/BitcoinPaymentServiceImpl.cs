using BitcoinPaymentService.Interfaces;

namespace BitcoinPaymentService.Services
{
    public class BitcoinPaymentServiceImpl : IBitcoinPaymentService
    {        public Task<string> MakePayment()
        {
            string paymentMessage = "Bitcoin Payment was successful!";
            return Task.FromResult(paymentMessage);
        }
    }
}
