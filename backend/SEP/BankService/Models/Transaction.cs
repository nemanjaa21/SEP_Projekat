using BankService.Enums;

namespace BankService.Models
{
    public class Transaction : EntityBase
    {
        public decimal Amount { get; set; }
        public long MerchantOrderId { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public long? AcquirerOrderId { get; set; }
        public DateTime? AcquirerTimestamp { get; set; }
        public long? IssuerOrderId { get; set; }
        public DateTime? IssuerTimestamp { get; set; }
        public Status Status { get; set; }
        public string? PaymentId { get; set; }
        public string? AcquirerAccountNumber { get; set; }
        public string? IssuerAccountNumber { get; set; }
        public string? Merchant_Id { get; set; }
        public int IdMerchant { get; set; }
        public Merchant? Merchant { get; set; }
        public int? IdUser { get; set; }
        public User? User { get; set; }
    }
}
