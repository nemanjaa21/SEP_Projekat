namespace BitcoinPaymentService.Interfaces
{
    public interface IBitcoinPaymentService
    {
        Task<string> MakePayment();
    }
}
