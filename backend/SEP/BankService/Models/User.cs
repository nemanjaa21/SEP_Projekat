namespace BankService.Models
{
    public class User : EntityBase
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public List<Account>? Accounts { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
