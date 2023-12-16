using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Interfaces
{
    public interface IMerchantService
    {
        Task<Merchant> GetMerchantById(string merchantId);
        Task<string> GenerateApiKey (string merchantId, string merchantPassword);
        Task<Merchant> GetMercantByApiKey(string apiKey);
        Task<string> RegisterMerchant(Merchant newMerchant);
    }
}
