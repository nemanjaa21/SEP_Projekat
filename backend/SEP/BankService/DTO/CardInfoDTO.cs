namespace BankService.DTO
{
    public class CardBase
    {
        public string? Pan { get; set; }
        public string? SecurityCode { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
    }

    public class NewCardDTO : CardBase
    {
        public int AccountId { get; set; }
    }

    public class CardInfoDTO : CardBase
    {
        public string? PaymentId { get; set; }
    }
}
