namespace QRCodePaymentService.Interfaces
{
    public interface IQRCodePaymentService
    {
        Task<string> MakePayment();
    }
}
