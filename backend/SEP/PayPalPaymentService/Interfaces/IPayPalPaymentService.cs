namespace PayPalPaymentService.Interfaces
{
    public interface IPayPalPaymentService
    {
        Task<string> MakePayment();
    }
}
