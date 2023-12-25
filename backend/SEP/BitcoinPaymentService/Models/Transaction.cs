using BitcoinPaymentService.Enums;

namespace BitcoinPaymentService.Models
{
    public class Transaction : EntityBase
    {
        public decimal Amount { get; set; }
        public long MerchantOrderId { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public string? PaymentId { get; set; }
        public string? Merchant_Id { get; set; }
        public int IdMerchant { get; set; }
        public string? UniqueHash { get; set; }
        public Status Status { get; set; }
        public Merchant? Merchant { get; set; }
        public int IdUser { get; set; }
        public User? User { get; set; }
    }
}
