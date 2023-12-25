using BankService.Enums;

namespace BankService.DTO
{
    public class QRCodeDataDTO
    {
        public Currency Currency { get; set; }
        public string? MerchantAccount {  get; set; }
        public int UserId { get; set; }
        public string? UserAccount { get; set; }
        public string? PaymentId { get; set; }
    }
}
