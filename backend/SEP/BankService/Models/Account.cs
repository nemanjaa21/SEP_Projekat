namespace BankService.Models
{
    public class Account : EntityBase
    {
        public string? AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Reserved { get; set; }
        public string? Merchant_Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }
        public List<Card>? Cards { get; set; }
    }
}
