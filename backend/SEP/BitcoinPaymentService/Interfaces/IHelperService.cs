using BitcoinPaymentService.Models;

namespace BitcoinPaymentService.Interfaces
{
    public interface IHelperService
    {
        double GetPrice(Merchant merchant);
    }
}
