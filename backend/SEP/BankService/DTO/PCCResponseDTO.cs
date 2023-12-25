using System.Runtime.InteropServices;

namespace BankService.DTO
{
    public class PCCResponseDTO
    {

        public string? Pan { get; set; }
        public string? SecurityCode { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public long AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public long IssuerOrderId { get; set; }
        public DateTime IssuerTimestamp { get; set; }
        public string? BankId { get; set; }
        public long MerchantOrderId { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public string? PaymentId { get; set; }
        public string? AcquirerAccountNumber { get; set; }
        public string? IssuerAccountNumber { get; set; }

        public PCCResponseDTO()
        {

        }

        public PCCResponseDTO(string? pan, string? securityCode, string? cardHolderName, string? expirationDate, decimal amount, long acquirerOrderId, DateTime acquirerTimestamp, long issuerOrderId, DateTime issuerTimestamp, string? bankId, long merchantOrderId, DateTime merchantTimestamp, string? paymentId, string? acquirerAccountNumber, string? issuerAccountNumber)
        {
            Pan = pan;
            SecurityCode = securityCode;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            Amount = amount;
            AcquirerOrderId = acquirerOrderId;
            AcquirerTimestamp = acquirerTimestamp;
            IssuerOrderId = issuerOrderId;
            IssuerTimestamp = issuerTimestamp;
            BankId = bankId;
            MerchantOrderId = merchantOrderId;
            MerchantTimestamp = merchantTimestamp;
            PaymentId = paymentId;
            AcquirerAccountNumber = acquirerAccountNumber;
            IssuerAccountNumber = issuerAccountNumber;
        }
    }
}
