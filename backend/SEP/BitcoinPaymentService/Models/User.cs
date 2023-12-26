namespace BitcoinPaymentService.Models
{
    public class User : EntityBase
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public string? EthereumAddress { get; set; }
    }
}
