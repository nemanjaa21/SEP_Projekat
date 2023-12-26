using BitcoinPaymentService.Interfaces;
using BitcoinPaymentService.Models;

namespace BitcoinPaymentService.Services
{
    public class HelperService : IHelperService
    {
        public double GetPrice(Merchant merchant)
        {
            return 100; //merchant.Price - (merchant.Price * merchant.Discount / 100);
        }
    }
}
