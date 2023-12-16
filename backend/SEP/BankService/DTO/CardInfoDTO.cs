namespace BankService.DTO
{
    public class CardInfoDTO
    {
        public string? Pan { get; set; }
        public string? SecurityCode { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
        public string? PaymentId { get; set; }
    }
}
