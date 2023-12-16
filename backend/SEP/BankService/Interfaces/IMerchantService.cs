using BankService.Models;

namespace BankService.Interfaces
{
    public interface IMerchantService
    {
        public Task<Merchant> GetByMerchantId(string id);
    }
}
