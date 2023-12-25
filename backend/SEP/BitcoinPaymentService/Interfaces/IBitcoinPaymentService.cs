using BitcoinPaymentService.DTO;

namespace BitcoinPaymentService.Interfaces
{
    public interface IBitcoinPaymentService
    {
        Task<decimal> GetPriceInEth(double price);
        Task<EthereumPaymentDTO> CreateEthereumPayment(int merchantId, int userId);
        Task CheckEthereumPayment(String transactionHash);
        Task CancelEthereumPayment(int merchantId);
        Task<string> MakePayment();
    }
}
