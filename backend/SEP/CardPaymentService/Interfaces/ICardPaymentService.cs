namespace CardPaymentService.Interfaces
{
    public interface ICardPaymentService
    {
        Task<string> MakePayment();
    }
}
