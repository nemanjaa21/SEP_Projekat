namespace BankService.Models
{
    public class Merchant : EntityBase
    {
        public string? FullName { get; set; }
        public string? MerchantId { get; set; }
        public string? MerchantPassword { get; set; }
        public string? ApiKey { get; set; }
        public List<Account>? Accounts { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
