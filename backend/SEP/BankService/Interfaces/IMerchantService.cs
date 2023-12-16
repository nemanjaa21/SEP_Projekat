using BankService.Models;

namespace BankService.Interfaces
{
    public interface IMerchantService
    {
        public Task<Merchant> GetById(int id);
    }
}
