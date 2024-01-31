namespace BankService.Models
{
    public class Card : EntityBase
    {
        public byte[]? Pan { get; set; }
        public byte[]? SecurityCode { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
