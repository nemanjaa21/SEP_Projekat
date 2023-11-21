namespace BankService.Models
{
    public class Merchant : EntityBase
    {
        public string? Merchant_Id { get; set; }
        public string? MerchantPassword { get; set; }
        public List<Account>? Accounts { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
