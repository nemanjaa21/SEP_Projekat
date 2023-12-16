using BankService.Models;

namespace BankService.Interfaces
{
    public interface ITransactionService
    {
        Task <Transaction> GetByPaymentId(string paymentId);
        Task<Transaction> GetById(int id);
    }
}
