using QRCodePaymentService.Interfaces;

namespace QRCodePaymentService.Services
{
    public class QRCodePaymentServiceImpl : IQRCodePaymentService
    {
        public Task<string> MakePayment()
        {
            string paymentMessage = "QRCode Payment was successful!";
            return Task.FromResult(paymentMessage);
        }
    }
}
