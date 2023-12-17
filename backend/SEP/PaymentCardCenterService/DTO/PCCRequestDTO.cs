namespace PaymentCardCenterService.DTO
{
    public class PCCRequestDTO
    {
        public string? Pan { get; set; }
        public string? SecurityCode { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public long AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public string? BankId { get; set; }
        public long MerchantOrderId { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public string? PaymentId { get; set; }
        public string? AcquirerAccountNumber { get; set; }
        public PCCRequestDTO(string? pan, string? securityCode, string? cardHolderName, string? expirationDate, decimal amount, long acquirerOrderId, DateTime acquirerTimestamp, string? bankId, long merchantOrderId, DateTime merchantTimestamp, string? paymentId, string? acquirerAccountNumber)
        {
            Pan = pan;
            SecurityCode = securityCode;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            Amount = amount;
            AcquirerOrderId = acquirerOrderId;
            AcquirerTimestamp = acquirerTimestamp;
            BankId = bankId;
            MerchantOrderId = merchantOrderId;
            MerchantTimestamp = merchantTimestamp;
            PaymentId = paymentId;
            AcquirerAccountNumber = acquirerAccountNumber;
        }
    }
}
