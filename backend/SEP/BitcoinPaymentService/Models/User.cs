using System.ComponentModel.DataAnnotations;

namespace BitcoinPaymentService.Models
{
    public class User : EntityBase
    {
        public string? Email { get; set; } = null!;
        public List<Transaction>? Transactions { get; set; }
        public string? EthereumAddress { get; set; }
    }
}
