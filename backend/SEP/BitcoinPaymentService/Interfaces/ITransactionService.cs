using BitcoinPaymentService.Models;

namespace BitcoinPaymentService.Interfaces
{
    public interface ITransactionService
    {
        Task <Transaction> GetByPaymentId(string paymentId);
        Task<Transaction> GetById(int id);
        Task<Transaction> MakeTransaction(int merchantId, int userId);
    }
}
