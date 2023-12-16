namespace BankService.DTO
{
    public class CardInfoDTO
    {
        public string? PAN { get; set; }
        public string? SecurityCode { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
        public int PaymentId { get; set; }
    }
}
