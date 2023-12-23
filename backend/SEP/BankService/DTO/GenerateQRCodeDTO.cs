using BankService.Enums;

namespace BankService.DTO
{
    public class GenerateQRCodeDTO
    {
        public int MerchantId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
